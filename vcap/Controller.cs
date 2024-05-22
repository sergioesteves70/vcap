using AForge.Video;

namespace vcap
{
    // Implementa a interface ICameraController, fornece uma classe concreta que gere a captura de vídeo e o processamento de imagens.
    public class CameraController : ICameraController
    {
        // Campo privado que armazena a instância de ICameraModel, utilizada para atribuir operações de câmera.
        private ICameraModel model;

        // Construtor da classe CameraController, aceita uma instância de ICameraModel como parâmetro,
        // permite que diferentes implementações de ICameraModel sejam usadas.
        public CameraController(ICameraModel model)
        {
            this.model = model; // Atribui o model passado ao campo privado.
        }

        // Implementação do método GetCameraNames da interface ICameraController
        // Chama o método correspondente no model para obter os nomes das câmeras disponíveis
        public string[] GetCameraNames()
        {
            return model.GetCameraNames();
        }

        // Implementação do método StartCamera da interface ICameraController
        // Verifica se há câmeras disponíveis antes de iniciar a captura
        // Se não houver câmeras, lança uma exceção
        // Caso contrário, delega a chamada ao método StartCamera do model
        public void StartCamera(int index, NewFrameEventHandler captureHandler)
        {
            if (model.Cameras.Count == 0)
            {
                // Lança uma exceção se nenhuma câmera for encontrada.
                throw new InvalidOperationException("Nenhuma câmera disponível. Verifique se uma câmera está conectada.");
            }

            model.StartCamera(index, captureHandler); // Inicia a captura de vídeo da câmera especificada.
        }

        // Implementação do método StopCamera da interface ICameraController
        public void StopCamera()
        {
            model.StopCamera();
        }

        // Implementação do método ApplyGrayscaleFilter da interface ICameraController
        // Chama o método correspondente no model para aplicar o filtro à imagem fornecida
        public Bitmap ApplyGrayscaleFilter(Bitmap image)
        {
            return model.ApplyGrayscaleFilter(image);
        }
    }
}
