using AForge.Video;

namespace vcap
{
    public class CameraController
    {
        private CameraModel model;

        public CameraController()
        {
            model = new CameraModel();
        }

        public string[] GetCameraNames()
        {
            return model.GetCameraNames();
        }
        
        public void StartCamera(int index, NewFrameEventHandler captureHandler)
        {
            model.StartCamera(index, captureHandler);
        }

        public void StopCamera()
        {
            model.StopCamera();
        }
    }
}
