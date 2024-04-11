using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiRestCurso.Models
{
    public class Categorias
    {
        public int CategoriaId { get; set; }
        public int IdProductos { get; set; }

        public string CategoriaNombre { get; set; }
        public string Nombre { get; set; }

        public static IEnumerable<Categorias> ObtenerCategorias(int iProducto, ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerCategoriasPorProducto", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductos", iProducto);


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);


                List<Models.Categorias> ListaCategorias = new List<Models.Categorias>();

                foreach (DataRow row in dt.Rows)
                {
                    var Categoria = new Categorias();
                    Categoria.Nombre = row["Nombre"].ToString();
                    Categoria.CategoriaNombre = row["Categorias"].ToString();
                    Categoria.CategoriaId = int.Parse(row["id_categoria"].ToString());
                    Categoria.IdProductos = int.Parse(row["ProductoID"].ToString());


                    ListaCategorias.Add(Categoria);
                }


                sResult = "";
                return ListaCategorias;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }

        public static IEnumerable<Categorias> ObtenerCategorias(ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerCategoriasNoticias", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);


                List<Models.Categorias> ListaCategorias = new List<Models.Categorias>();

                foreach (DataRow row in dt.Rows)
                {
                    var Categoria = new Categorias();
                    Categoria.CategoriaId = int.Parse(row["Id"].ToString());
                    Categoria.CategoriaNombre = (row["Categorias"].ToString());

                    ListaCategorias.Add(Categoria);
                }


                sResult = "";
                return ListaCategorias;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }

        public static IEnumerable<Categorias> ObtenerTodasLasCategorias(ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerCategoriasDisponiblesPorProducto", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;



                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);


                List<Models.Categorias> ListaCategorias = new List<Models.Categorias>();

                foreach (DataRow row in dt.Rows)
                {
                    var Categoria = new Categorias();
                    Categoria.CategoriaId = int.Parse(row["id_categoria"].ToString());
                    Categoria.CategoriaNombre = row["CategoriaNombre"].ToString();


                    ListaCategorias.Add(Categoria);
                }


                sResult = "";
                return ListaCategorias;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }


        public static IEnumerable<Categorias> ObtenerCategoriasDisponibles(int iProducto, ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerCategoriasDisponiblesPorProducto", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@id_producto", iProducto);


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);
                List<Models.Categorias> ListaCategorias = new List<Models.Categorias>();

                foreach (DataRow row in dt.Rows)
                {
                    var Categoria = new Categorias();
                    Categoria.CategoriaId = int.Parse(row["CategoriaId"].ToString());
                    Categoria.CategoriaNombre = row["CategoriaNombre"].ToString();
                    Categoria.IdProductos = int.Parse(row["Productoid"].ToString());
                    Categoria.Nombre = row["Nombre"].ToString();


                    ListaCategorias.Add(Categoria);
                }


                sResult = "";
                return ListaCategorias;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }

    }






}