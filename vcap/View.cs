using System.Drawing.Imaging;
using AForge.Video;

namespace vcap
{
    public partial class View : Form
    {
        // Declara��o das vari�veis
        private readonly ICameraController controller; // Controlador da c�mera
        private readonly string path = @"C:\uab\LDS\Atividades\efolioC\vcap\imagens\"; // Caminho para a pasta de imagens

        // Construtor da classe View
        public View()
        {
            InitializeComponent();
            // Inicializar o modelo da c�mera e do controlador da c�mera
            ICameraModel model = new CameraModel();
            controller = new CameraController(model);
            // Carregar as c�meras dispon�veis
            CarregaCameras();
        }

        // M�todo para carregar as c�meras dispon�veis no ComboBox
        private void CarregaCameras()
        {
            comboBox1.Items.AddRange(controller.GetCameraNames()); // Adicionar os nomes das c�meras ao ComboBox
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0; // Selecionar a primeira c�mera, se houver
        }

        // Evento clicar no bot�o para iniciar a c�mera
        private void Button1_Click(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex; // Obter o �ndice da c�mera selecionada
            try
            {
                controller.StartCamera(i, CapturaCam); // Iniciar a c�mera com o �ndice selecionado
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Erro ao iniciar a c�mera", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // M�todo de captura de imagem da c�mera
        private void CapturaCam(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagem = (Bitmap)eventArgs.Frame.Clone(); // Clonar o frame recebido
            pictureBox1.Image = Imagem; // Exibir a imagem no PictureBox
        }

        // Evento clicar no bot�o para parar a c�mera
        private void button5_Click(object sender, EventArgs e)
        {
            controller.StopCamera(); // Parar c�mera
        }

        // Evento clicar no bot�o para copiar imagem
        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) // Verificar se h� uma imagem no PictureBox
            {
                pictureBox2.Image = pictureBox1.Image; // Copiar a imagem para o segundo PictureBox
                string filePath = System.IO.Path.Combine(path, "frame.jpg"); // Caminho do ficheiro de imagem
                pictureBox2.Image.Save(filePath, ImageFormat.Jpeg); // guaradar a imagem como JPEG
            }
        }

        // M�todo para abrir uma pasta e selecionar uma imagem
        private void OpenFolderAndSelectImage()
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog()) // Abrir o seletor de pasta
            {
                folderDialog.Description = "Selecione a pasta contendo a imagem";
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    using (OpenFileDialog fileDialog = new OpenFileDialog()) // Abrir o seletor de arquivo
                    {
                        fileDialog.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                        fileDialog.Title = "Selecione a imagem";
                        fileDialog.InitialDirectory = folderDialog.SelectedPath;

                        DialogResult fileResult = fileDialog.ShowDialog();

                        if (fileResult == DialogResult.OK && !string.IsNullOrWhiteSpace(fileDialog.FileName))
                        {
                            try
                            {
                                Image selectedImage = Image.FromFile(fileDialog.FileName); // Carregar a imagem selecionada
                                pictureBox2.Image = selectedImage; // Exibir a imagem no segundo PictureBox
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Erro ao carregar a imagem. Verifique se o arquivo � v�lido.", "Erro ao carregar imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        // Evento clicar no bot�o para abrir uma imagem
        private void buttonOpenImage_Click(object sender, EventArgs e)
        {
            OpenFolderAndSelectImage(); // Abrir a pasta e seleciona a imagem
        }

        // Evento clicar no bot�o para aplicar filtro de escala de cinza
        private void button6_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null) // Verificar se h� uma imagem no segundo PictureBox
            {
                try
                {
                    Bitmap filteredImage = controller.ApplyGrayscaleFilter((Bitmap)pictureBox2.Image.Clone()); // Aplicar o filtro de escala de cinza
                    pictureBox2.Image = filteredImage; // Exibir a imagem filtrada no segundo PictureBox

                    using (SaveFileDialog saveDialog = new SaveFileDialog()) // Abrir o seletor de local para guardar a imagem
                    {
                        saveDialog.Filter = "Imagem JPEG|*.jpg";
                        saveDialog.Title = "Salvar imagem como...";
                        saveDialog.FileName = "imagem_filtrada";

                        if (saveDialog.ShowDialog() == DialogResult.OK)
                        {
                            string savePath = saveDialog.FileName;
                            filteredImage.Save(savePath, ImageFormat.Jpeg); // GUardar a imagem filtrada
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ocorreu um erro ao aplicar o filtro ou salvar a imagem. Por favor, tente novamente.", "Erro ao aplicar filtro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("N�o h� imagem para aplicar o filtro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Evento clicar no bot�o para sair da aplica��o
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Fechar aplica��o
        }

        // Evento chamado quando o formul�rio est� prestes a ser fechado
        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            controller.StopCamera(); // Parar c�mera
        }

        // Evento chamado quando o formul�rio � carregado
        private void Vcap_Load(object sender, EventArgs e)
        {

        }
    }
}
