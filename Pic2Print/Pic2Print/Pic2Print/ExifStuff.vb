Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

Module ExifStuff
    ' Orientations.
    Private Const OrientationId As Integer = &H112
    Private Const DateTimeId As Integer = &H9003

    Public Enum ExifOrientations As Byte
        Unknown = 0
        TopLeft = 1
        TopRight = 2
        BottomRight = 3
        BottomLeft = 4
        LeftTop = 5
        RightTop = 6
        RightBottom = 7
        LeftBottom = 8
    End Enum

    ' Make an image to demonstrate orientations.
    Public Function OrientationImage(ByVal orientation As ExifOrientations) As Image
        Const size As Integer = 64
        Dim bm As New Bitmap(size, size)
        Using gr As Graphics = Graphics.FromImage(bm)
            gr.Clear(Color.White)
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit

            ' Orient the result.
            Select Case (orientation)
                Case ExifOrientations.TopLeft
                Case ExifOrientations.TopRight
                    gr.ScaleTransform(-1, 1)
                Case ExifOrientations.BottomRight
                    gr.RotateTransform(180)
                Case ExifOrientations.BottomLeft
                    gr.ScaleTransform(1, -1)
                Case ExifOrientations.LeftTop
                    gr.RotateTransform(90)
                    gr.ScaleTransform(-1, 1, MatrixOrder.Append)
                Case ExifOrientations.RightTop
                    gr.RotateTransform(-90)
                Case ExifOrientations.RightBottom
                    gr.RotateTransform(90)
                    gr.ScaleTransform(1, -1, MatrixOrder.Append)
                Case ExifOrientations.LeftBottom
                    gr.RotateTransform(90)
            End Select

            ' Translate the result to the center of the bitmap.
            gr.TranslateTransform(size / 2, size / 2, MatrixOrder.Append)
            Using string_format As New StringFormat()
                string_format.LineAlignment = StringAlignment.Center
                string_format.Alignment = StringAlignment.Center
                Using the_font As New Font("Times New Roman", 40, GraphicsUnit.Point)
                    If (orientation = ExifOrientations.Unknown) Then
                        gr.DrawString("?", the_font, Brushes.Black, 0, 0, string_format)
                    Else
                        gr.DrawString("F", the_font, Brushes.Black, 0, 0, string_format)
                    End If
                End Using
            End Using
        End Using

        Return bm
    End Function

    ' Return the image's orientation.
    Public Function ImageOrientation(ByVal img As Image) As ExifOrientations
        ' Get the index of the orientation property.
        Dim orientation_index As Integer = Array.IndexOf(img.PropertyIdList, OrientationId)

        ' If there is no such property, return Unknown.
        If (orientation_index < 0) Then Return ExifOrientations.Unknown

        ' Return the orientation value.
        Return DirectCast(img.GetPropertyItem(OrientationId).Value(0), ExifOrientations)
    End Function

    ' Orient the image properly.
    Public Sub OrientImage(ByVal img As Image)
        ' Get the image's orientation.
        Dim orientation As ExifOrientations = ImageOrientation(img)

        ' Orient the image.
        Select Case (orientation)
            Case ExifOrientations.Unknown
            Case ExifOrientations.TopLeft
            Case ExifOrientations.TopRight
                img.RotateFlip(RotateFlipType.RotateNoneFlipX)
            Case ExifOrientations.BottomRight
                img.RotateFlip(RotateFlipType.Rotate180FlipNone)
            Case ExifOrientations.BottomLeft
                img.RotateFlip(RotateFlipType.RotateNoneFlipY)
            Case ExifOrientations.LeftTop
                img.RotateFlip(RotateFlipType.Rotate90FlipX)
            Case ExifOrientations.RightTop
                img.RotateFlip(RotateFlipType.Rotate90FlipNone)
            Case ExifOrientations.RightBottom
                img.RotateFlip(RotateFlipType.Rotate90FlipY)
            Case ExifOrientations.LeftBottom
                img.RotateFlip(RotateFlipType.Rotate270FlipNone)
        End Select

        ' Set the image's orientation to TopLeft.
        SetImageOrientation(img, ExifOrientations.TopLeft)
    End Sub

    ' Orient the image properly.
    Public Sub GetImageTakenDate(ByVal img As Image, ByRef trg As String, ByRef Year As Integer, ByRef Mon As Integer, ByRef day As Integer)
        Dim propItems As PropertyItem() = img.PropertyItems
        'Convert the value of the second property to a string, and display it. 
        Dim encoding As New System.Text.ASCIIEncoding()
        Dim propItem As PropertyItem

        ' default to 1980
        Year = 1980
        Mon = 1
        day = 1
        trg = "1980:01:01 00:00:01"     ' default windows date YYYY:MM:DD HH:MM:SS

        ' look through all elements to find the date, then extract the numbers
        For Each propItem In propItems

            If propItem.Id = DateTimeId Then

                trg = encoding.GetString(propItem.Value)
                Year = CInt(Mid(trg, 1, 4))
                Mon = CInt(Mid(trg, 6, 2))
                day = CInt(Mid(trg, 9, 2))

                Exit For

            End If

        Next propItem

        ' show what we found
        'MsgBox(trg)

    End Sub

    ' Set the image's orientation.
    Public Sub SetImageOrientation(ByVal img As Image, ByVal orientation As ExifOrientations)
        ' Get the index of the orientation property.
        Dim orientation_index As Integer = Array.IndexOf(img.PropertyIdList, OrientationId)

        ' If there is no such property, do nothing.
        If (orientation_index < 0) Then Return

        ' Set the orientation value.
        Dim item As PropertyItem = img.GetPropertyItem(OrientationId)
        item.Value(0) = orientation
        img.SetPropertyItem(item)
    End Sub
End Module

