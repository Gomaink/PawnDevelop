/*
 * Created by SharpDevelop.
 * User: Samuel
 * Date: 24/03/2021
 * Time: 07:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using DiscordRpcDemo;

namespace PawnCoders_IDE
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>

	public partial class MainForm : Form
	{
		private DiscordRpc.EventHandlers handlers;
		private DiscordRpc.RichPresence presence;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
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
		void FontesToolStripMenuItemClick(object sender, EventArgs e)
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
		void FerramentasToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void DesfazerToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void RefazerToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void CopiarToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void ColarToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void DeletarToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void SelecionarTudoToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void CompilarToolStripMenuItemClick(object sender, EventArgs e)
		{
			Compilar();
		}
		
		void AjudaToolStripMenuItemClick(object sender, EventArgs e)
		{
			MessageBox.Show("Todo suporte disponível em nosso discord\nhttps://discord.gg/kvgnyE92BK");
		}
		
		void RepositorioToolStripMenuItemClick(object sender, EventArgs e)
		{
			MessageBox.Show("PawnDevelop é um programa open-source\nhttps://github.com/Gomaink/PawnDevelop");
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
	    }
		private void OpenFile()
		{
			try
			{
				OpenFileDialog ofd =  new OpenFileDialog();
				if(ofd.ShowDialog()==DialogResult.OK)
				{
					StreamReader reader = new StreamReader(ofd.FileName);
					
					richTextBox1.Text = reader.ReadToEnd();
					MessageBox.Show(ofd.FileName);
					
					Global.diretorio = ofd.FileName;
				}
			}
			catch(Exception ex)
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
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.FileName = "E:/Programacao/bleh/pawno/pawncc.exe";


			startInfo.Arguments = String.Format("{0} -De:/Programacao/bleh/gamemodes -;+ -(+ -d3", Global.diretorio);

			//startInfo.Arguments = "e:/Programacao/bleh/gamemodes/main.pwn -De:/Programacao/bleh/gamemodes -;+ -(+ -d3";
			Process.Start(startInfo);
			richTextBox2.Text += "\n"+startInfo.ToString();
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
			this.handlers = default(DiscordRpc.EventHandlers);
			DiscordRpc.Initialize("833197504186023998", ref this.handlers, true, null);
			this.handlers = default(DiscordRpc.EventHandlers);
			DiscordRpc.Initialize("833197504186023998", ref this.handlers, true, null);
			this.presence.details = "Editando: ";
			this.presence.state = "Em: ";
			this.presence.largeImageKey = "pawncoders";
			this.presence.smallImageKey = "pawn";
			this.presence.startTimestamp = 1507665886;
			this.presence.endTimestamp = 1507665886;
			this.presence.largeImageText = "Editando um arquivo .pwn";
			this.presence.smallImageText = "Pawn Develop v0.1";
			DiscordRpc.UpdatePresence(ref this.presence);
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
			richTextBox2.BackColor = lgray;

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
			fontesToolStripMenuItem.ForeColor = white;
			escolherTemaToolStripMenuItem.ForeColor = white;

			fontesToolStripMenuItem.BackColor = black;
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
			desfazerToolStripMenuItem.BackColor = black;
			refazerToolStripMenuItem.BackColor = black;
			colarToolStripMenuItem.BackColor = black;
			copiarToolStripMenuItem.BackColor = black;
			deletarToolStripMenuItem.BackColor = black;
			selecionarTudoToolStripMenuItem.BackColor = black;

			desfazerToolStripMenuItem.ForeColor = white;
			refazerToolStripMenuItem.ForeColor = white;
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
			richTextBox2.BackColor = lgray;

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
			fontesToolStripMenuItem.ForeColor = black;
			escolherTemaToolStripMenuItem.ForeColor = black;

			fontesToolStripMenuItem.BackColor = white;
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
			desfazerToolStripMenuItem.BackColor = white;
			refazerToolStripMenuItem.BackColor = white;
			colarToolStripMenuItem.BackColor = white;
			copiarToolStripMenuItem.BackColor = white;
			deletarToolStripMenuItem.BackColor = white;
			selecionarTudoToolStripMenuItem.BackColor = white;

			desfazerToolStripMenuItem.ForeColor = black;
			refazerToolStripMenuItem.ForeColor = black;
			colarToolStripMenuItem.ForeColor = black;
			copiarToolStripMenuItem.ForeColor = black;
			deletarToolStripMenuItem.ForeColor = black;
			selecionarTudoToolStripMenuItem.ForeColor = black;
		}
    }
}
