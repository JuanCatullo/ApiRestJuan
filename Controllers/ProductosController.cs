using ApiRestCurso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;

namespace ApiRestCurso.Controllers
{
    public class ProductosController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Models.Producto> Get()
        {
            //return new string[] { "value1", "value2" };

            string sRet = "";
            List<Models.Producto> ListaProductos = (List<Producto>)Producto.ObtenerProductos(-1, ref sRet);

            return ListaProductos;
        }

        [HttpGet]
        //[Route("ApiCurso/Productos/Get/")]
        public Models.Producto ObtenerProducto(int id)
        {
            //return new string[] { "Producto 1", "Producto 2" };
            string sRet = "";
            Models.Producto DetalleProducto = (Producto)Producto.ObtenerProducto(id, -1, ref sRet);

            return DetalleProducto;
        }

        [HttpPost]
        public IHttpActionResult InsertarProducto([FromBody] Models.Producto NuevoProducto)
        {
            //CODIGO PARA INSERTAR UN PRODUCTO

            string sRet = "";
            int iProductoID = 0;

            sRet = Models.Producto.InsertarProducto(NuevoProducto, ref iProductoID);


            //Si salio todo ok, le agrego el ID que obtuvo al objeto original
            if (sRet == "")
            {
                NuevoProducto.IdProductos = iProductoID;
            }
            else
            {
                return BadRequest("Error al insertar el producto: " + sRet);
            }

            //return BadRequest("Not a valid model");
            //return NotFound();



            return Ok(NuevoProducto);
            //return HttpStatusCode.Created;
            //return new ContentResult() { Content = value, new HttpStatusCode(HttpStatusCode.Created) };
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult ModificarProducto([FromBody] Models.Producto ProductoExistente)
        {
            //CODIGO PARA MODIFICAR UN PRODUCTO
            string sRet = "";

            sRet = Models.Producto.ModificarProducto(ProductoExistente);


            //Si salio todo ok, le agrego el ID que obtuvo al objeto original
            if (sRet == "")
            {
                //NuevoProducto.id = iProductoID;
            }
            else
            {
                return BadRequest("Error al modificar el producto: " + sRet);
            }

            //return BadRequest("Not a valid model");
            //return NotFound();



            return Ok(ProductoExistente);


        }



        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult EliminarProducto(int id)
        {
            string sRet = "";

            sRet = Models.Producto.EliminarProducto(id);
            if (sRet == "")
            {
                //NuevoProducto.id = iProductoID;
            }
            else
            {
                return BadRequest("Error al eliminar el producto: " + sRet);
            }

            return Ok("Se borro");
        }

        [HttpGet]
        [System.Web.Http.Route("ApiRestCurso/TodasLasCategorias")]
        public IEnumerable<Models.Categorias> TodasLasCategorias()
        {


            string sRet = "";
            List<Models.Categorias> ListaCategorias = (List<Categorias>)Categorias.ObtenerTodasLasCategorias(ref sRet);

            return ListaCategorias;
        }



        [HttpGet]
        [System.Web.Http.Route("ApiRestCurso/Categorias")]
        public IEnumerable<Models.Categorias> ObtenerCategorias()
        {
            //return new string[] { "value1", "value2" };

            string sRet = "";
            List<Models.Categorias> ListaCategorias = (List<Categorias>)Categorias.ObtenerCategorias(ref sRet);

            return ListaCategorias;
        }

        [HttpGet]
        [System.Web.Http.Route("ApiRestCurso/Categorias")]
        public IEnumerable<Models.Categorias> ObtenerCategorias(int ProductoId)
        {
            //return new string[] { "value1", "value2" };

            string sRet = "";
            List<Models.Categorias> ListaCategorias = (List<Categorias>)Categorias.ObtenerCategorias(ProductoId, ref sRet);

            return ListaCategorias;
        }



        [HttpPost]
        [System.Web.Http.Route("ApiRestCurso/AgregarCategoriaProducto")]
        public IHttpActionResult EliminarCategoriaProducto(int id_producto, int id_categoria)
        {
            string sRet = "";

            sRet = Models.Producto.EliminarCategoriaProducto(id_producto, id_categoria);
            if (sRet == "")
            {
                //NuevoProducto.id = iProductoID;
            }
            else
            {
                return BadRequest("Error al eliminar la categoria para el producto: " + sRet);
            }

            return Ok("Se borro");
        }

        // DELETE api/<controller>/5
        [HttpPost]
        [System.Web.Http.Route("api/AgregarCategoriaProducto")]
        public IHttpActionResult AgregarCategoriaProducto(int producto_id, int categoria_id)
        {
            string sRet = "";

            sRet = Models.Producto.InsertarCategoriaProducto(producto_id, categoria_id);
            if (sRet == "")
            {
                //NuevoProducto.id = iProductoID;
            }
            else
            {
                return BadRequest("Error al insertar categoria al producto: " + sRet);
            }

            return Ok("Se agrego categoria");
        }

        [HttpGet]
        [System.Web.Http.Route("api/CategoriasDisponibles")]
        public IEnumerable<Models.Categorias> ObtenerCategoriasDisponibles(int Productoid)
        {

            string sRet = "";
            List<Models.Categorias> ListaCategorias = (List<Categorias>)Categorias.ObtenerCategoriasDisponibles(Productoid, ref sRet);


            //TODO: MOSTRAR MENSAJE
            if (ListaCategorias.Count == 0)
            {

            }

            return ListaCategorias;
        }



    }
}