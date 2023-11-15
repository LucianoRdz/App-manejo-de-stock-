using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TPFinalNivel2_Rodriguez
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }
        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            try
            {
                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember= "Descripcion";
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txbCodigo.Text = articulo.Codigo;
                    txbNombre.Text = articulo.Nombre;
                    txbDescripcion.Text = articulo.Descripcion;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    txbImagenUrl.Text = articulo.ImagenUrl;
                    cargarImagen(articulo.ImagenUrl);
                    txbPrecio.Text = articulo.Precio.ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        private void txbImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txbImagenUrl.Text);
        }


        private void cargarImagen (string imagen)
        {
            try
            {
                pbxImagen.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxImagen.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

           ArticuloNegocio negocio = new ArticuloNegocio();
            
            try
            {
                if (validarDatos())
                    return;
               
                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txbCodigo.Text;
                articulo.Nombre = txbNombre.Text;
                articulo.Descripcion = txbDescripcion.Text;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.ImagenUrl = txbImagenUrl.Text;
                articulo.Precio = decimal.Parse(txbPrecio.Text);

                if (articulo.Id != 0 )
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado correctamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado correctamente");
                }
                


                Close();
            }
            catch (Exception ex)
            {

               throw ex;
            }
            
        }

        //Validaciones

        public bool validarDatos()
        {
            if (string.IsNullOrEmpty(txbCodigo.Text))
            {
                MessageBox.Show("Por favor inserte Codigo de articulo");
                return (true);
            }
            if (string.IsNullOrEmpty(txbNombre.Text))
            {
                MessageBox.Show("Por favor inserte Nombre de articulo");
                return (true);
            }
            if (string.IsNullOrEmpty(txbImagenUrl.Text))
            {
                MessageBox.Show("Por favor inserte la Url de imagen del articulo");
                return (true);
            }
            if (string.IsNullOrEmpty(txbPrecio.Text))
            {
                MessageBox.Show("Por favor inserte Precio de articulo");
                return (true);
            }
            if (string.IsNullOrEmpty(txbDescripcion.Text))
            {
                MessageBox.Show("Por favor inserte una descripcion de articulo");
                return (true);

            }
            return false;


        }
    }
}
