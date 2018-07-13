Public Class ImageCaching
    '==========================================================================================================================
    '==========================================< IMAGE MANAGEMENT >============================================================
    '==========================================================================================================================

    'Private _index As Integer                       ' points to the internal array location
    Private _FileNamesMax As Integer = 0            ' max filename index in array
    Private _FileNamesHighPrint As Integer = 0      ' highest index in filenames thats been printed

    Private _filePath As String                     ' this instance's path to the files 
    Private _FileName() As String               ' string filenames
    Private _FileNamePrinted() As Integer       ' # of times printed
    Private _FileNameEmail() As String          ' user email addresses
    Private _FileNamePhone() As String          ' user phone number
    Private _FileNamePhoneSel() As Integer      ' user phone number selector
    Private _FileNameMessage() As String        ' a message the user can send/print on photos
    Private _FileNameOptIn() As Boolean         ' User opts-in to email lists
    Private _FileNamePermit() As Boolean        ' User gives permission to use his/her photo in promos
    Private _MaxArraySize = 50                  ' this will rise to 2048

    ' tracking of image resources
    Private _ImageCacheFileName(14) As String      ' file names matching the image
    Private _ImageCachePtr(14) As Image            ' pointers to images for tracking resources
    Private _ImageCacheAllocFlag(14) As Integer    ' 0=free space, 1=holding, 2=inuse
    Private _ImageCacheTimeToLive(14) As Integer   ' count down to free it up

    Public Sub New()
        Call resizeArrays(128)
        reset()
    End Sub

    Private Sub resizeArrays(ByVal siz As Integer)
        'Return 0

        _MaxArraySize = siz
        ReDim Preserve _FileName(siz)               ' string filenames
        ReDim Preserve _FileNamePrinted(siz)        ' # of times printed
        ReDim Preserve _FileNameEmail(siz)          ' user email addresses
        ReDim Preserve _FileNamePhone(siz)          ' user phone number
        ReDim Preserve _FileNamePhoneSel(siz)       ' user phone number selector
        ReDim Preserve _FileNameMessage(siz)        ' a message the user can send/print on photos
        ReDim Preserve _FileNameOptIn(siz)          ' OptIn to emails
        ReDim Preserve _FileNamePermit(siz)         ' permits image to be used

    End Sub

    ' index property to index all the rest of the data - file names, etc.
    'Public Property index() As Integer
    'Get
    '    Return _index
    'End Get
    'Set(ByVal value As Integer)
    '    _index = value
    'End Set
    'End Property

    ' maximum number of file names cached
    Public ReadOnly Property maxIndex() As Integer
        Get
            Return _FileNamesMax
        End Get
        'Set(ByVal value As Integer)
        '    If _FileNamesMax < 2048 Then
        '        _FileNamesMax = value
        '    End If
        ' End Set

    End Property

    ' the file names printed count
    Public Property maxPrintCount(ByVal i As Integer) As Integer
        Get
            Return _FileNamePrinted(i)
        End Get
        Set(ByVal value As Integer)
            _FileNamePrinted(i) = value
            If i > _FileNamesHighPrint Then
                _FileNamesHighPrint = i
            End If
        End Set
    End Property

    ' the highest index that has a print count
    Property maxPrintedIndex() As Integer
        Get
            Return _FileNamesHighPrint
        End Get
        Set(ByVal value As Integer)
            _FileNamesHighPrint = value
        End Set
    End Property

    ' list of file names with counts
    Property filePath() As String
        Get
            Return _filePath
        End Get

        Set(ByVal value As String)
            _filePath = value
        End Set

    End Property

    ' list of file names with counts
    Property fileName(ByVal i As Integer) As String
        Get
            Return _FileName(i)
        End Get

        Set(ByVal value As String)
            _FileName(i) = value
        End Set

    End Property

    ' list of file names with counts
    Public ReadOnly Property fullName(ByVal i As Integer) As String
        Get
            Return _filePath & _FileName(i)
        End Get

    End Property

    ' list of file names with counts
    Property message(ByVal i As Integer) As String
        Get
            Return _FileNameMessage(i)
        End Get

        Set(ByVal value As String)
            _FileNameMessage(i) = value
        End Set

    End Property


    ' Permits image to be used in promos
    Property OptIn(ByVal i As Integer) As Boolean
        Get
            Return _FileNameOptIn(i)
        End Get

        Set(ByVal value As Boolean)
            _FileNameOptIn(i) = value
        End Set

    End Property

    ' Permits image to be used in promos
    Property permit(ByVal i As Integer) As Boolean
        Get
            Return _FileNamePermit(i)
        End Get

        Set(ByVal value As Boolean)
            _FileNamePermit(i) = value
        End Set

    End Property

    ' list of emails for the indexed file name
    Property emailAddr(ByVal i As Integer) As String
        Get
            Return _FileNameEmail(i)
        End Get

        Set(ByVal value As String)
            _FileNameEmail(i) = value
        End Set

    End Property

    ' list of emails for the indexed file name
    Property phoneNumber(ByVal i As Integer) As String
        Get
            Return _FileNamePhone(i)
        End Get

        Set(ByVal value As String)
            _FileNamePhone(i) = value
        End Set

    End Property

    ' Carrier selector to the combobox carrier list
    Property carrierSelector(ByVal i As Integer) As Integer
        Get
            Return _FileNamePhoneSel(i)
        End Get
        Set(ByVal value As Integer)
            _FileNamePhoneSel(i) = value
        End Set
    End Property

    Public Function full() As Boolean
        If _FileNamesMax = _MaxArraySize Then
            Return True
        End If
        Return False
    End Function

    Public Function matchFound(ByRef fnam As String) As Boolean

        ' if no data, return not found
        If _FileNamesMax = 0 Then Return False

        For n = 0 To _FileNamesMax - 1
            If fnam = _FileName(n) Then Return True
        Next

        Return False

    End Function

    Public Function newItem() As Integer
        Dim idx As Integer
        idx = Me._FileNamesMax
        _FileNamesMax += 1
        If _FileNamesMax = _MaxArraySize Then
            Call resizeArrays(_MaxArraySize + 128)
        End If
        Return idx
    End Function
    ' Return a pointer to a cached image.  If not cached, load it in, cache it, then return the pointer
    Public Function FetchPicture(ByRef fnam As String) As Image
        Dim found As Int16
        Dim idx As Int16
        Dim srcImg As String

        ' Bad Dog! No Null ptrs!
        If fnam = "" Then
            Globals.fDebug.txtPrintLn("FetchPicture: Don't call this will NULL names")
            Return Nothing
        End If

        ' see if we already have it in cache, if so, return the ptr
        For idx = 0 To 13
            If _ImageCacheFileName(idx) = fnam Then
                _ImageCacheAllocFlag(idx) = 2     ' make active again
                'Globals.fDebug.txtPrintLn("FetchPicture: " & fnam & " found")
                Return _ImageCachePtr(idx)
            End If
        Next

        ' not in cache, we have to make sure we now have space in the cache so time something out
        For idx = 0 To 13
            If TimeOutDisposeUnused() Then
                Exit For
            End If
        Next

        ' find that free'd location. Gotta rescan for it..
        found = -1
        For idx = 0 To 13
            If _ImageCacheAllocFlag(idx) = 0 Then
                found = idx
                Exit For
            End If
        Next

        ' make sure that worked..
        If found = -1 Then
            'Globals.fDebug.TxtPrint("FetchPicture: Cache is full! Nothing timed out," & vbCrLf & _
            '        "No space free to add new image!")
            Return Nothing
        End If

        ' Specify a valid picture file path on your computer - Get path + file name.jpg
        'srcImg = Globals.tmpIncoming_Folder & fnam
        srcImg = _filePath & fnam

        ' read in the new image, cache it
        _ImageCacheFileName(idx) = fnam
        _ImageCacheAllocFlag(idx) = 2

        ' we use this approach to lock the files while in use to avoid UAEs DSC-CRASH
        Try
            _ImageCachePtr(idx) = Image.FromFile(srcImg)
            'Globals.fDebug.txtPrintLn("FetchPicture: Loaded " & fnam & " from file.")
            'return freshly loaded image
            Return _ImageCachePtr(idx)
        Catch ex As Exception
            Globals.fDebug.TxtPrint("Image.FromFile exception:," & ex.ToString)
            Return Nothing
        End Try

    End Function

    ' free up picture, but don't dispose of it yet. Alloc flag goes from 2 to 1
    Public Sub FreePicture(ByRef fnam As String)
        Static Counter As Int16 = 14
        'Dim found As Int16 = -1
        Dim idx As Int16

        Counter -= 1
        If Counter = 0 Then Counter = 14

        ' process only valid file names
        If fnam <> "" Then

            ' find the matching picture, and set the flag to free, but held state
            For idx = 0 To 13
                If _ImageCacheFileName(idx) = fnam Then
                    _ImageCacheAllocFlag(idx) = 1              ' available to be freed when needed, but keep it around for a while
                    _ImageCacheTimeToLive(idx) = Counter       ' never twice the same value means randomness on timeouting resources
                    'found = 1
                    Exit For
                End If
            Next

        End If

    End Sub

    ' free up picture, but don't dispose of it yet
    Public Sub FreeAllPictures()
        Dim idx As Int16

        For idx = 0 To 13
            If _ImageCacheAllocFlag(idx) = 2 Then
                FreePicture(_ImageCacheFileName(idx))
            End If
        Next

    End Sub

    Public Sub reset()
        Dim i As Integer
        FreeAllPictures()
        For i = 0 To _MaxArraySize - 1
            maxPrintCount(i) = 0
            fileName(i) = ""
        Next
        _FileNamesMax = 0
        maxPrintedIndex = 0
    End Sub

    ' flush this image out of the cache
    Public Function FlushNamed(ByRef nam As String) As Boolean
        Dim idx As Integer
        Dim b As Boolean = False

        ' loop through find all locations set to one, then free them up
        For idx = 0 To 13
            ' if found release everything
            If _ImageCacheFileName(idx) = nam Then
                _ImageCacheFileName(idx) = ""
                _ImageCacheAllocFlag(idx) = 0
                _ImageCacheTimeToLive(idx) = 0
                _ImageCachePtr(idx).Dispose()
                b = True
                Exit For
            End If
        Next

        ' return whether we found it in the cache or not..
        Return b

    End Function

    ' flush this image out of the cache
    Public Function FindByName(ByRef nam As String) As Boolean
        Dim idx As Integer
        Dim b As Boolean = False

        ' loop through find all locations for the file name, return true if found
        For idx = 0 To 13
            ' if found release everything
            If _ImageCacheFileName(idx) = nam Then
                b = True
                Exit For
            End If
        Next

        ' return whether we found it in the cache or not..
        Return b

    End Function

    ' Force the disposal of unused, but held image resources
    Public Sub ForceDisposeUnused()
        Dim found As Integer = -1
        Dim idx As Integer

        ' loop through find all locations set to one, then free them up
        For idx = 0 To 13
            If _ImageCacheAllocFlag(idx) = 1 Then
                _ImageCacheAllocFlag(idx) = 0
                _ImageCachePtr(idx).Dispose()
                _ImageCacheFileName(idx) = ""
            End If
        Next

    End Sub

    ' Timeout the idle state of unused resources and dispose if timed out
    Public Function TimeOutDisposeUnused() As Boolean
        Dim found As Boolean = False
        Dim idx As Integer

        ' loop through find all locations set to one, if timed out, then free them up
        For idx = 0 To 13

            ' free space is an automatic win
            If _ImageCacheAllocFlag(idx) = 0 Then
                found = True    ' this works too, we find one already freed up
            End If

            ' timeout the inactive storage
            If _ImageCacheAllocFlag(idx) = 1 Then
                ' first if there is a timer, count it down
                If (_ImageCacheTimeToLive(idx) > 0) Then
                    _ImageCacheTimeToLive(idx) -= 1
                End If
                ' if timer is zero, free it up
                If _ImageCacheTimeToLive(idx) = 0 Then
                    _ImageCacheAllocFlag(idx) = 0
                    _ImageCachePtr(idx).Dispose()
                    _ImageCacheFileName(idx) = ""
                    found = True
                End If
            End If
        Next

        ' true if we freed up something, false if all is full
        Return found

    End Function

End Class
