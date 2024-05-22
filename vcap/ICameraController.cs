using AForge.Video; // Importa o namespace AForge.Video, que contém classes para captura e processamento de vídeo

namespace vcap // Define um namespace chamado 'vcap'
{
    // Declaração da interface ICameraController. Interfaces definem contratos que as classes devem seguir,
    // especificando métodos e propriedades que a classe deve implementar
    public interface ICameraController
    {
        // Método que retorna um array de strings com os nomes das câmeras disponíveis no sistema,
        // e o utilizador pode selecionar.
        string[] GetCameraNames();

        // Método para iniciar a captura de vídeo de uma câmera escolhida.
        // O parâmetro 'index' indica qual câmera selecionar (caso, existam várias câmeras ligadas).
        // O parâmetro 'captureHandler' é um delegado que aponta para um método que será chamado sempre que um novo frame de vídeo for capturado.
        void StartCamera(int index, NewFrameEventHandler captureHandler);

        // Método para parar a captura de vídeo da câmera atualmente em uso.
        // Essencial para libertar recursos quando a captura está OFF.
        void StopCamera();

        // Método para aplicar o filtro grayscale numa imagem.
        // Recebe um objeto 'Bitmap' que representa a imagem original e retorna um novo 'Bitmap' com filtro aplicado.        
        Bitmap ApplyGrayscaleFilter(Bitmap image);
    }
}
