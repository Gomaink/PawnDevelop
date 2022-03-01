using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using DiscordRPC;

namespace PawnCoders_IDE
{
	public partial class MainForm : Form
	{
		public DiscordRpcClient client;
		public MainForm()
		{
			InitializeComponent();
		}
		void NovoToolStripMenuItemClick(object sender, EventArgs e)
		{
			CriarNovo();
		}
		
		void AbrirToolStripMenuItemClick(object sender, EventArgs e)
		{
			OpenFile();
		}
		
		void SalvarToolStripMenuItemClick(object sender, EventArgs e)
		{
			SaveFile();
		}
		
		void SairToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(!string.IsNullOrEmpty(this.richTextBox1.Text))
			{
				SaveFile();
			}
			else
			{
				this.Close();
			}
		}

		void FerramentasToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void CopiarToolStripMenuItemClick(object sender, EventArgs e)
		{
			richTextBox1.Copy();
		}
		
		void ColarToolStripMenuItemClick(object sender, EventArgs e)
		{
			richTextBox1.Paste();
		}
		
		void DeletarToolStripMenuItemClick(object sender, EventArgs e)
		{
			richTextBox1.SelectedText = "";
		}
		
		void SelecionarTudoToolStripMenuItemClick(object sender, EventArgs e)
		{
			richTextBox1.SelectAll();
		}
		
		void CompilarToolStripMenuItemClick(object sender, EventArgs e)
		{
			Compilar();
		}
		
		void AjudaToolStripMenuItemClick(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Todo suporte disponível em nosso discord.\n\nhttps://discord.gg/kvgnyE92BK");
			if (result == DialogResult.OK)
			{
				Process.Start("https://discord.gg/kvgnyE92BK");
			}
		}

		void RepositorioToolStripMenuItemClick(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Pawn Develop é um programa open-source.\n\nhttps://github.com/Gomaink/PawnDevelop");
			if (result == DialogResult.OK)
			{
				Process.Start("https://github.com/Gomaink/PawnDevelop");
			}
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			OpenFile();
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			SaveFile();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			MessageBox.Show("Entre em nosso forúm\nDisponivel em breve!");
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			Compilar();
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			CriarNovo();
		}
		
		//METODO PARA ABRIR O ARQUIVO
		public static class Global
	    {
	        public static string diretorio;
			public static string nome;
	    }
		private void OpenFile()
		{
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					StreamReader reader = new StreamReader(ofd.FileName);

					richTextBox1.Text = reader.ReadToEnd();

					Global.diretorio = ofd.FileName;

					char[] delimiterChars = { '\\' };
					string[] words = Global.diretorio.Split(delimiterChars);
					int total = words.Length - 1;
					int total2 = words.Length - 3;

					client.SetPresence(new RichPresence()
					{
						Details = $"Trabalhando em {words[total2]}",
						State = $"Editando {words[total]}",
						Timestamps = Timestamps.Now,
						Assets = new Assets()
						{
							LargeImageKey = "pawn",
							LargeImageText = "Editando um arquivo .pwn"
						}
					});

				}
			}

			catch (Exception ex)
			{
				MessageBox.Show("Erro: " + ex.Message);
			}
		}
		
		//METODO PARA SALVAR O ARQUIVO
		private void SaveFile()
		{
			try
			{
				SaveFileDialog sfd = new SaveFileDialog();
				
				if(sfd.ShowDialog()==DialogResult.OK)
				{
					StreamWriter writer = new StreamWriter(sfd.FileName);
					
					if(!string.IsNullOrEmpty(richTextBox1.Text))
					{
						writer.Write(richTextBox1.Text);
						writer.Close();
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("Erro: " + ex.Message);
			}
		}
		
		private void CriarNovo()
		{
			try
			{
				if(!string.IsNullOrEmpty(this.richTextBox1.Text))
				{
					this.Text = "Sem Titulo";
					this.richTextBox1.Text = string.Empty;
				}
				else
				{
					MessageBox.Show("Não há nada para salvar\nVocê já pode fechar esta janela!");
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("Erro: " + ex.Message);
			}
			
		}
		
		private void Compilar()
        {
			try
			{
				string[] dir = Global.diretorio.Split('\\');

				string s1 = "";
				string s2 = "";

				int totals = -1;
				int totals2 = -1;

				while (totals != dir.Length - 2)
				{
					totals++;
					s1 += $"{dir[totals]}";
					if (totals != dir.Length - 2)
						s1 += "/";
				}

				while (totals2 != dir.Length - 3)
				{
					totals2++;
					s2 += $"{dir[totals2]}";
					if (totals2 != dir.Length - 3)
						s2 += "/";

					if (totals2 == dir.Length - 3)
						s2 += "/pawno/pawncc.exe";
				}

				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.FileName = $"{s2}";

				//startInfo.Arguments = "dir/main.pwn -Ddir/gamemodes -;+ -(+ -d3";
				startInfo.Arguments = String.Format($"{Global.diretorio} -D{s1} -;+ -(+ -d3");

				Process.Start(startInfo);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Erro: " + ex.Message);
			}
		}

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

		}

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

		private void MainForm_Load(object sender, EventArgs e)
		{
			client = new DiscordRpcClient("833197504186023998");
			client.Initialize();

			client.SetPresence(new RichPresence()
			{
				Details = "Trabalhando em /",
				State = "Editando /",
				Timestamps = Timestamps.Now,
				Assets = new Assets()
				{
					LargeImageKey = "pawn",
					LargeImageText = "Ausente"
				}
			});
		}

        private void escolherTemaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void escuroToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Color white = Color.FromName("ControlLight");
			Color black = Color.FromArgb(31, 31, 31);
			richTextBox1.BackColor = black;
			richTextBox1.ForeColor = white;

			Color lgray = Color.FromArgb(64, 64, 64);


			//Alterando as cores dos menus's
			arquivoToolStripMenuItem.BackColor = black;
			editarToolStripMenuItem.BackColor = black;
			ferramentasToolStripMenuItem.BackColor = black;
			compiladorToolStripMenuItem.BackColor = black;
			sobreToolStripMenuItem.BackColor = black;

			arquivoToolStripMenuItem.ForeColor = white;
			editarToolStripMenuItem.ForeColor = white;
			ferramentasToolStripMenuItem.ForeColor = white;
			compiladorToolStripMenuItem.ForeColor = white;
			sobreToolStripMenuItem.ForeColor = white;

			//Alterando a cor da barra dos menu's
			menuStrip1.BackColor = black;

			//Alterando as cores do menu "arquivo"
			novoToolStripMenuItem.BackColor = black;
			abrirToolStripMenuItem.BackColor = black;
			salvarToolStripMenuItem.BackColor = black;
			sairToolStripMenuItem.BackColor = black;

			novoToolStripMenuItem.ForeColor = white;
			abrirToolStripMenuItem.ForeColor = white;
			salvarToolStripMenuItem.ForeColor = white;
			sairToolStripMenuItem.ForeColor = white;

			//Alterando as cores do menu "ferramentas"
			definirArquivoToolStripMenuItem.ForeColor = white;
			escolherTemaToolStripMenuItem.ForeColor = white;

			definirArquivoToolStripMenuItem.BackColor = black;
			escolherTemaToolStripMenuItem.BackColor = black;

			//Alterando as cores do menu "compilador"
			compilarToolStripMenuItem.BackColor = black;

			compilarToolStripMenuItem.ForeColor = white;

			//Alterando as cores do menu "sobre"
			ajudaToolStripMenuItem.BackColor = black;
			repositorioToolStripMenuItem.BackColor = black;

			ajudaToolStripMenuItem.ForeColor = white;
			repositorioToolStripMenuItem.ForeColor = white;

			//Alterando as cores do menu "editar"
			colarToolStripMenuItem.BackColor = black;
			copiarToolStripMenuItem.BackColor = black;
			deletarToolStripMenuItem.BackColor = black;
			selecionarTudoToolStripMenuItem.BackColor = black;

			colarToolStripMenuItem.ForeColor = white;
			copiarToolStripMenuItem.ForeColor = white;
			deletarToolStripMenuItem.ForeColor = white;
			selecionarTudoToolStripMenuItem.ForeColor = white;
		}

        private void claroToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Color white = Color.FromName("White");
			richTextBox1.BackColor = white;
			Color black = Color.FromName("Black");
			richTextBox1.ForeColor = black;
			Color lgray = Color.FromName("LightGray");

			//Alterando as cores dos menus's
			arquivoToolStripMenuItem.BackColor = white;
			editarToolStripMenuItem.BackColor = white;
			ferramentasToolStripMenuItem.BackColor = white;
			compiladorToolStripMenuItem.BackColor = white;
			sobreToolStripMenuItem.BackColor = white;

			arquivoToolStripMenuItem.ForeColor = black;
			editarToolStripMenuItem.ForeColor = black;
			ferramentasToolStripMenuItem.ForeColor = black;
			compiladorToolStripMenuItem.ForeColor = black;
			sobreToolStripMenuItem.ForeColor = black;

			//Alterando a cor da barra dos menu's
			menuStrip1.BackColor = white;

			//Alterando as cores do menu "arquivo"
			novoToolStripMenuItem.BackColor = white;
			abrirToolStripMenuItem.BackColor = white;
			salvarToolStripMenuItem.BackColor = white;
			sairToolStripMenuItem.BackColor = white;

			novoToolStripMenuItem.ForeColor = black;
			abrirToolStripMenuItem.ForeColor = black;
			salvarToolStripMenuItem.ForeColor = black;
			sairToolStripMenuItem.ForeColor = black;

			//Alterando as cores do menu "ferramentas"
			definirArquivoToolStripMenuItem.ForeColor = black;
			escolherTemaToolStripMenuItem.ForeColor = black;

			definirArquivoToolStripMenuItem.BackColor = white;
			escolherTemaToolStripMenuItem.BackColor = white;

			//Alterando as cores do menu "compilador"
			compilarToolStripMenuItem.BackColor = white;

			compilarToolStripMenuItem.ForeColor = black;

			//Alterando as cores do menu "sobre"
			ajudaToolStripMenuItem.BackColor = white;
			repositorioToolStripMenuItem.BackColor = white;

			ajudaToolStripMenuItem.ForeColor = black;
			repositorioToolStripMenuItem.ForeColor = black;

			//Alterando as cores do menu "editar"
			colarToolStripMenuItem.BackColor = white;
			copiarToolStripMenuItem.BackColor = white;
			deletarToolStripMenuItem.BackColor = white;
			selecionarTudoToolStripMenuItem.BackColor = white;
			cortarToolStripMenuItem.BackColor = white;

			colarToolStripMenuItem.ForeColor = black;
			copiarToolStripMenuItem.ForeColor = black;
			deletarToolStripMenuItem.ForeColor = black;
			selecionarTudoToolStripMenuItem.ForeColor = black;
			cortarToolStripMenuItem.ForeColor = black;
		}

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
			richTextBox1.Cut();
		}

        private void definirArquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					StreamReader reader = new StreamReader(ofd.FileName);
					Global.diretorio = ofd.FileName;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Erro: " + ex.Message);
			}
		}
    }
}
