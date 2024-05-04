using AForge.Imaging.Filters;
using AForge.Video.DirectShow;
using AForge.Video;

namespace vcap
{
    public class CameraModel
    {
        private FilterInfoCollection Cameras;
        private VideoCaptureDevice? CameraEscolhida;


        public CameraModel()
        {
            CarregaCameras();
        }

        private void CarregaCameras()
        {
            Cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        public string[] GetCameraNames()
        {
            string[] cameraNomes = new string[Cameras.Count];
            for (int i = 0; i < Cameras.Count; i++)
            {
                cameraNomes[i] = Cameras[i].Name.ToString();
            }
            return cameraNomes;
        }

        public void StartCamera(int index, NewFrameEventHandler captureHandler)
        {
            if (Cameras.Count > 0)
            {
                ExitCam();
                string NomeVideo = Cameras[index].MonikerString;
                CameraEscolhida = new VideoCaptureDevice(NomeVideo);
                CameraEscolhida.NewFrame += captureHandler;
                CameraEscolhida.Start();
            }
        }

        public void StopCamera()
        {
            if (CameraEscolhida != null && CameraEscolhida.IsRunning)
            {
                CameraEscolhida.SignalToStop();
            }
        }

        private void ExitCam()
        {
            if (CameraEscolhida != null && CameraEscolhida.IsRunning)
            {
                CameraEscolhida.SignalToStop();
                CameraEscolhida = null;
            }
        }

        // Método para aplicar um filtro grayscale à imagem
        public Bitmap ApplyGrayscaleFilter(Bitmap image)
        {
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            return filter.Apply(image);
        }

    }
}
