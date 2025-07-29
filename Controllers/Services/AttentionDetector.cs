using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

public class AttentionDetector
{
    private readonly CascadeClassifier _faceCascade;
    private readonly CascadeClassifier _eyeCascade;

    public AttentionDetector()
    {
        _faceCascade = new CascadeClassifier("HaarCascades/haarcascade_frontalface_default.xml");
        _eyeCascade = new CascadeClassifier("HaarCascades/haarcascade_eye.xml");
    }

    public bool IsPersonAttentive(string imagePath)
    {
        using var image = new Image<Bgr, byte>(imagePath);
        using var gray = image.Convert<Gray, byte>();

        var faces = _faceCascade.DetectMultiScale(gray, 1.1, 4);
        foreach (var face in faces)
        {
            var faceRegion = new Rectangle(face.X, face.Y, face.Width, face.Height);
            var faceROI = gray.GetSubRect(faceRegion);
            var eyes = _eyeCascade.DetectMultiScale(faceROI);

            if (eyes.Length >= 2)
                return true; // Person is likely attentive
        }

        return false; // Not attentive
    }
}
