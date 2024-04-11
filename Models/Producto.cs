using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiRestCurso.Models
{
    public class Producto
    {
        public int IdProductos { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int id_categoria { get; set; }

        public string DescripcionError { get; set; }
        public static IEnumerable<Producto> ObtenerProductos(int iCategoria, ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerProductos", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@id_categoria", iCategoria);


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);


                List<Models.Producto> ListaProductos = new List<Models.Producto>();

                foreach (DataRow row in dt.Rows)
                {
                    var Producto = new Producto();
                    Producto.IdProductos = int.Parse(row["IdProductos"].ToString());
                    Producto.Nombre = row["Nombre"].ToString();
                    Producto.Descripcion = row["Descripcion"].ToString();
                    Producto.Precio = decimal.Parse(row["Precio"].ToString());
                    Producto.Stock = int.Parse(row["Stock"].ToString());
                    Producto.id_categoria = int.Parse(row["id_categoria"].ToString());
                    Producto.DescripcionError = "";

                    ListaProductos.Add(Producto);
                }


                sResult = "";
                return ListaProductos;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }


        public static Producto ObtenerProducto(int iProductoid, int CategoriaId, ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerProducto", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@id_categoria", CategoriaId);
                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductos", iProductoid);


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);


                var Producto = new Producto();


                //PROBLEMA DE LOGICA NO EXISTE EL PRODUCTO
                if (dt.Rows.Count == 0)
                {
                    Producto.DescripcionError = "No se encontró el producto!";
                }


                foreach (DataRow row in dt.Rows)
                {

                    Producto.IdProductos = int.Parse(row["IdProductos"].ToString());
                    Producto.Descripcion = row["Nombre"].ToString();
                    Producto.Nombre = row["Descripción"].ToString();
                    Producto.Precio = decimal.Parse(row["Precio"].ToString());
                    Producto.Stock = int.Parse(row["Stock"].ToString());
                    Producto.id_categoria = int.Parse(row["id_categoria"].ToString());
                    Producto.DescripcionError = "";
                }


                sResult = "";
                return Producto;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }




        public static string InsertarProducto(Models.Producto nuevoProducto, ref int iProductoID)
        {
            string sRet = "";

            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MySqlCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MySqlCommand = new SqlCommand("spInsertarProducto", MyConnection);
                MySqlCommand.CommandType = CommandType.StoredProcedure;


                MySqlCommand.Parameters.AddWithValue("@Nombre", nuevoProducto.Nombre);
                MySqlCommand.Parameters.AddWithValue("@Descripción", nuevoProducto.Descripcion);
                MySqlCommand.Parameters.AddWithValue("@Precio", nuevoProducto.Precio);
                MySqlCommand.Parameters.AddWithValue("@Stock", nuevoProducto.Stock);
                MySqlCommand.Parameters.AddWithValue("@id_categoria", nuevoProducto.id_categoria);


                // Agrego los Parámetros al SPROC (OUT)

                SqlParameter pariProductoID = new SqlParameter("@ProductoID", SqlDbType.Int);
                pariProductoID.Direction = ParameterDirection.Output;

                MySqlCommand.Parameters.Add(pariProductoID);



                MyConnection.Open();
                MySqlCommand.ExecuteNonQuery();

                //OBTENGO LOS VALORES DE LOS PARAMETROS DE SALIDA
                iProductoID = int.Parse(pariProductoID.Value.ToString());

                MyConnection.Close();
                MyConnection.Dispose();


                sRet = "";

            }
            catch (Exception ex)
            {
                sRet = ex.Message;

            }



            return sRet;
        }



        public static string ModificarProducto(Models.Producto ProductoExistente)
        {
            string sRet = "";

            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MySqlCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MySqlCommand = new SqlCommand("spModificarProducto", MyConnection);
                MySqlCommand.CommandType = CommandType.StoredProcedure;



                MySqlCommand.Parameters.AddWithValue("@IdProductos", ProductoExistente.IdProductos);
                MySqlCommand.Parameters.AddWithValue("@Nombre", ProductoExistente.Nombre);
                MySqlCommand.Parameters.AddWithValue("@Descripción", ProductoExistente.Descripcion);
                MySqlCommand.Parameters.AddWithValue("@Precio", ProductoExistente.Precio);
                MySqlCommand.Parameters.AddWithValue("@Stock", ProductoExistente.Stock);
                MySqlCommand.Parameters.AddWithValue("@id_categoria", ProductoExistente.id_categoria);



                MyConnection.Open();
                MySqlCommand.ExecuteNonQuery();

                MyConnection.Close();
                MyConnection.Dispose();


                sRet = "";

            }
            catch (Exception ex)
            {
                sRet = ex.Message;

            }



            return sRet;
        }


        public static string EliminarProducto(int id)
        {
            string sRet = "";

            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MySqlCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MySqlCommand = new SqlCommand("spEliminarProducto", MyConnection);
                MySqlCommand.CommandType = CommandType.StoredProcedure;



                MySqlCommand.Parameters.AddWithValue("@id", id);



                MyConnection.Open();
                MySqlCommand.ExecuteNonQuery();

                MyConnection.Close();
                MyConnection.Dispose();


                sRet = "";

            }
            catch (Exception ex)
            {
                sRet = ex.Message;

            }



            return sRet;
        }

        public static string EliminarCategoriaProducto(int id_producto, int id_categoria)
        {
            string sRet = "";

            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MySqlCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MySqlCommand = new SqlCommand("spEliminarCategoriaPorProducto", MyConnection);
                MySqlCommand.CommandType = CommandType.StoredProcedure;



                MySqlCommand.Parameters.AddWithValue("@id_producto", id_producto);
                MySqlCommand.Parameters.AddWithValue("@id_categoria", id_categoria);



                MyConnection.Open();
                MySqlCommand.ExecuteNonQuery();

                MyConnection.Close();
                MyConnection.Dispose();


                sRet = "";

            }
            catch (Exception ex)
            {
                sRet = ex.Message;

            }



            return sRet;
        }

        public static string InsertarCategoriaProducto(int iProductoID, int id_categoria)
        {
            string sRet = "";

            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MySqlCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MySqlCommand = new SqlCommand("spInsertarCategoriaPorProducto", MyConnection);
                MySqlCommand.CommandType = CommandType.StoredProcedure;


                MySqlCommand.Parameters.AddWithValue("@id_producto", iProductoID);
                MySqlCommand.Parameters.AddWithValue("@id_categoria", id_categoria);

                MyConnection.Open();
                MySqlCommand.ExecuteNonQuery();

             

                MyConnection.Close();
                MyConnection.Dispose();


                sRet = "";

            }
            catch (Exception ex)
            {
                sRet = ex.Message;

            }



            return sRet;
        }


    }
}