using AForge.Video;
using AForge.Video.DirectShow;

namespace vcap
{
    public interface ICameraModel
    {
        // FilterInfoCollection - retorna informações sobre as câmeras disponíveis no sistema
        FilterInfoCollection Cameras { get; }

        // Método que retorna um array de strings com os nomes das câmeras disponíveis no sistema
        string[] GetCameraNames();

        // Método para iniciar a captura de vídeo de uma câmera escolhida
        void StartCamera(int index, NewFrameEventHandler captureHandler);

        // Método para parar a captura de vídeo da câmera atualmente em uso
        void StopCamera();

        // Método para aplicar o filtro grayscale numa imagem.
        Bitmap ApplyGrayscaleFilter(Bitmap image);
    }
}
