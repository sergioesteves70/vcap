using AForge.Imaging.Filters; 
using AForge.Video.DirectShow;
using AForge.Video;
using System.Timers;
using System.Drawing;
using System.IO;

namespace vcap
{
    // Implementa a interface ICameraModel, fornece uma classe concreta que gere câmeras e processamento de imagens.
    public class CameraModel : ICameraModel
    {
        // Campo privado para armazenar a câmera escolhida para captura de vídeo
        private VideoCaptureDevice? CameraEscolhida;

        // Campo privado readonly para armazenar uma instância do filtro grayscale
        private readonly Grayscale grayscaleFilter;

        // Campo privado para um timer que controla a captura automática de frames.
        private System.Timers.Timer captureTimer;

        // Diretório onde os frames capturados são guardados.
        private readonly string captureDirectory = @"C:\uab\LDS\Atividades\efolioC\vcapMVC\imagens\auto\";

        // Propriedade pública que retorna as câmeras disponíveis no sistema.
        public FilterInfoCollection Cameras { get; private set; }

        // Construtor da classe CameraModel. Inicia as câmeras disponíveis, o filtro grayscale e o timer.
        public CameraModel()
        {
            Cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721); // Inicia o filtro grayscale com coeficientes de luminância.
            captureTimer = new System.Timers.Timer(20000); // Inicia o timer para capturar frames a cada 20 segundos.
            captureTimer.Elapsed += CaptureTimerElapsed;

            // Cria a pasta de captura se não existir.
            if (!Directory.Exists(captureDirectory))
            {
                Directory.CreateDirectory(captureDirectory);
            }
        }

        // Método para obter o nome das câmeras disponíveis.
        public string[] GetCameraNames()
        {
            string[] cameraNomes = new string[Cameras.Count];
            for (int i = 0; i < Cameras.Count; i++)
            {
                cameraNomes[i] = Cameras[i].Name.ToString(); // Preencher o array com os nomes das câmeras
            }
            return cameraNomes;
        }

        // Método para iniciar a captura de vídeo de uma câmera específica
        public void StartCamera(int index, NewFrameEventHandler captureHandler)
        {
            if (Cameras.Count > 0)
            {
                ExitCam(); // Parar câmera que esteja ON
                string NomeVideo = Cameras[index].MonikerString; // Obtém a string que identifica a câmera selecionada
                CameraEscolhida = new VideoCaptureDevice(NomeVideo);
                CameraEscolhida.NewFrame += captureHandler; // Associa o manipulador de eventos para novos frames
                CameraEscolhida.Start(); // Inicia a captura de vídeo
                captureTimer.Start(); // Inicia o timer para capturar frames automaticamente
            }
        }

        // Método para parar a captura de vídeo.
        public void StopCamera()
        {
            captureTimer.Stop(); // Parar o timer de captura automática.
            if (CameraEscolhida != null && CameraEscolhida.IsRunning)
            {
                CameraEscolhida.SignalToStop(); // Parar a captura de vídeo.
            }
        }

        // Método auxiliar para parar a câmera atual, se estiver ON.
        private void ExitCam()
        {
            if (CameraEscolhida != null && CameraEscolhida.IsRunning)
            {
                CameraEscolhida.SignalToStop();
                CameraEscolhida = null;
            }
        }

        // Método chamado para capturar um frame automaticamente.
        private void CaptureTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (CameraEscolhida != null && CameraEscolhida.IsRunning)
            {
                CameraEscolhida.NewFrame -= SaveFrameHandler; // Remove qualquer manipulador de eventos anterior.
                CameraEscolhida.NewFrame += SaveFrameHandler; // Adiciona um novo manipulador de eventos para guaradar os frames.
            }
        }

        // Manipulador de eventos para guardar os frames.
        private void SaveFrameHandler(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap frame = (Bitmap)eventArgs.Frame.Clone(); // Copia o frame atual.
            SaveFrame(frame); //Guarda o frame na pasta especificada.
            CameraEscolhida.NewFrame -= SaveFrameHandler; // Remove o manipulador de eventos depois de guardar o frame.
        }

        // Método para guardar um frame num ficheiro.
        private void SaveFrame(Bitmap frame)
        {
            string fileName = Path.Combine(captureDirectory, $"frame_{DateTime.Now:yyyyMMdd_HHmmss}.jpg"); // Gera um nome de ficheiro baseado na data e hora atual.
            frame.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg); // Guarda a imagem como JPEG.
            frame.Dispose(); // Liberta os recursos da imagem.
        }

        // Método para aplicar o filtro grayscale na imagem
        public Bitmap ApplyGrayscaleFilter(Bitmap image)
        {
            return grayscaleFilter.Apply(image); // Aplica o filtro e retorna a imagem com filtro aplicado
        }

    }
}
