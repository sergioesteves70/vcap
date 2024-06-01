using AForge.Video;

namespace vcap
{
    // Implementar a interface ICameraController, fornece uma classe concreta que gere a captura de vídeo e o processamento de imagens.
    public class CameraController(ICameraModel model) : ICameraController
    {
        // Armazenar a instância de ICameraModel, utilizada para atribuir operações de câmera.
        private readonly ICameraModel model = model;

        // Implementar o método GetCameraNames da interface ICameraController
        // Chamar o método correspondente no model para obter os nomes das câmeras disponíveis
        public string[] GetCameraNames()
        {
            return model.GetCameraNames();
        }

        // Implementar o método StartCamera da interface ICameraController
        // Verificar se há câmeras disponíveis antes de iniciar a captura
        // Se não houver câmeras, lança uma exceção
        // Caso contrário, delega a chamada ao método StartCamera do model
        public void StartCamera(int index, NewFrameEventHandler captureHandler)
        {
            if (model.Cameras.Count == 0)
            {
                // Lançar uma exceção se nenhuma câmera for encontrada.
                throw new InvalidOperationException("Nenhuma câmera disponível. Verifique se a câmera está conectada.");
            }

            model.StartCamera(index, captureHandler); // Inicia a captura de vídeo da câmera especificada.
        }

        // Implementar o método StopCamera da interface ICameraController
        public void StopCamera()
        {
            model.StopCamera();
        }

        // Implementar o método ApplyGrayscaleFilter da interface ICameraController
        // Chamar o método correspondente no model para aplicar o filtro à imagem fornecida
        public Bitmap ApplyGrayscaleFilter(Bitmap image)
        {
            return model.ApplyGrayscaleFilter(image);
        }
    }
}
