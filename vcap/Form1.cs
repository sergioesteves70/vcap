using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;

namespace vcap
{
    public partial class Form1 : Form
    {
        private CameraController controller;
        private string Path = @"C:\\uab\\LDS\\Atividades\\efolioB\\vcap\\imagens\\";
        private VideoCaptureDevice CameraEscolhida;

        public Form1()
        {
            InitializeComponent();
            controller = new CameraController();
            CarregaCameras();
        }

        private void CarregaCameras()
        {
            comboBox1.Items.AddRange(controller.GetCameraNames());
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            controller.StartCamera(i, CapturaCam);
        }

        private void CapturaCam(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagem = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = Imagem;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            controller.StopCamera();
        }

        // Clicar no botão para capturar uma imagem e gravar numa pasta
        private void button2_Click(object sender, EventArgs e)
        {
            if (CameraEscolhida != null && CameraEscolhida.IsRunning)
            {
                pictureBox2.Image = pictureBox1.Image;
                pictureBox2.Image.Save(Path + "frame.jpg", ImageFormat.Jpeg);
            }
        }

        // Método para abrir o explorador de pasta e selecionar uma imagem
        private void OpenFolderAndSelectImage()
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Definir a pasta
                folderDialog.Description = "Selecione a pasta contendo a imagem";
                // Exibir a janela diálogo
                DialogResult result = folderDialog.ShowDialog();

                // Verificar se o utilizador selecionou uma pasta
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    // Abre uma janela para selecionar a imagem dentro da pasta selecionada
                    using (OpenFileDialog fileDialog = new OpenFileDialog())
                    {
                        // Filtro para mostrar só imagens
                        fileDialog.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                        // Definir o nome da janela do diálogo
                        fileDialog.Title = "Selecione a imagem";
                        // Definir a pasta inicial como a pasta selecionada pelo Utilizador
                        fileDialog.InitialDirectory = folderDialog.SelectedPath;

                        // Exibir a janela diálogo de seleção de ficheiro
                        DialogResult fileResult = fileDialog.ShowDialog();

                        // Verificar se o utilizador selecionou uma imagem
                        if (fileResult == DialogResult.OK && !string.IsNullOrWhiteSpace(fileDialog.FileName))
                        {
                            // Abre a imagem na PictureBox2
                            pictureBox2.Image = Image.FromFile(fileDialog.FileName);
                        }
                    }
                }
            }
        }

        // Clicar no botão para abrir pasta e selecionar imagem
        private void buttonOpenFolderAndSelectImage_Click(object sender, EventArgs e)
        {
            OpenFolderAndSelectImage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFolderAndSelectImage();
        }

        // Evento de clicar no botão de aplicar filtro e guardar
        private void button6_Click(object sender, EventArgs e)
        {
            // Verificar se há uma imagem na PictureBox2
            if (pictureBox2.Image != null)
            {
                // Aplicar o filtro grayscale à imagem na PictureBox2
                Bitmap filteredImage = ApplyGrayscaleFilter((Bitmap)pictureBox2.Image.Clone());
                // Exibir a imagem filtrada na PictureBox2
                pictureBox2.Image = filteredImage;

                // Exibir o cx diálogo para guardar a imagem
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Imagem JPEG|*.jpg";
                    saveDialog.Title = "Salvar imagem como...";
                    saveDialog.FileName = "imagem_filtrada"; // Nome do ficheiro para gravar

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Guardar a imagem com filtro/processada com o nome escolhido pelo utilizador em formato JPG
                        string savePath = saveDialog.FileName;
                        filteredImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }
            else
            {
                MessageBox.Show("Não há imagem para aplicar o filtro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para guardar uma imagem
        public void SaveImage(Bitmap image)
        {
            string filePath = Path + "imagem_salva.jpg";
            image.Save(filePath, ImageFormat.Jpeg);
        }

        private Bitmap ApplyGrayscaleFilter(Bitmap bitmap)
        {
            throw new NotImplementedException();
        }

        // Método para sair da aplicação
        private void ExitApplication()
        {
            
            Application.Exit(); // Encerra a aplicação
        }

        // Evento de clique para o botão de saída
        private void button3_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
