using AForge.Video;

namespace vcap
{
    public interface ICameraController
    {
        // Retornar um array de strings com os nomes das câmeras disponíveis no sistema
        string[] GetCameraNames();

        // Iniciar a captura de vídeo de uma câmera escolhida
        void StartCamera(int index, NewFrameEventHandler captureHandler);

        // Parar a captura de vídeo da câmera atualmente em uso
        void StopCamera();

        // Aplicar o filtro grayscale numa imagem
        Bitmap ApplyGrayscaleFilter(Bitmap image);
    }
}
