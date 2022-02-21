
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using GS.TOOLS;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
//using System.Data.Entity;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Repositories
{
    public class InventarioRepository : Repository<InventarioVenta>, IInventarioRepository
    {
        public InventarioRepository(SIFMContext context) : base(context)
        {
        }

        public List<InventarioMovimientoDto> SelMovimientoEntrada(int TipoMovimiento, DateTime fechaInicio, DateTime fechaFin, long IdProducto, long IdBodega)
        {
            List<InventarioMovimientoDto> movimientoDtos = new List<InventarioMovimientoDto>();
            try
            {
                if (TipoMovimiento == 1 || TipoMovimiento == 99)
                {
                    List<InventarioMovimientoEntrada> InventarioMovimientoEntradas = _context.InventarioMovimientoEntrada
                        .Where(x => x.Estado == true)
                        .Where(x => x.IdProducto == IdProducto || IdProducto == 0)
                        .Where(x => x.IdInventarioBodega == IdBodega || IdBodega == 0)
                        .Where(x => x.FechaCreacion >= fechaInicio && x.FechaCreacion <= fechaFin)
                        .ToList();

                    if(InventarioMovimientoEntradas != null && InventarioMovimientoEntradas.Any())
                    {
                        foreach (var item in InventarioMovimientoEntradas)
                        {
                            InventarioMovimientoDto movimientoDto = new InventarioMovimientoDto
                            {
                                IdProducto = item.IdProducto,
                                Cantidad = item.Cantidad,
                                Precio = item.Precio,
                                NumeroFactura = item.NumeroFactura,
                                Cufe = item.Cufe,
                                IdClienteProveedor = item.IdProveedor,
                                Motivo = item.Motivo,
                                IdInventarioBodega = item.IdInventarioBodega,
                                Ip = item.Ip,
                                UsuarioCreacion = item.UsuarioCreacion,
                                FechaCreacion = item.FechaCreacion,
                                UsuarioModificacion = item.UsuarioModificacion,
                                FechaModificacion = item.FechaModificacion,
                                UsuarioEliminacion = item.UsuarioEliminacion,
                                FechaEliminacion = item.FechaEliminacion,
                                Estado = item.Estado,
                                IdInventarioMovimiento = item.IdInventarioMovimiento
                            };
                            movimientoDtos.Add(movimientoDto);
                        }
                    }
                }

                if (TipoMovimiento == 2 || TipoMovimiento == 99)
                {
                    List<InventarioMovimientoSalida> InventarioMovimientoSalidas = _context.InventarioMovimientoSalida
                        .Where(x => x.Estado == true)
                        .Where(x => x.IdProducto == IdProducto || IdProducto == 0)
                        .Where(x => x.IdInventarioBodega == IdBodega || IdBodega == 0)
                        .Where(x => x.FechaCreacion >= fechaInicio && x.FechaCreacion <= fechaFin)
                        .ToList();

                    if (InventarioMovimientoSalidas != null && InventarioMovimientoSalidas.Any())
                    {
                        foreach (var item in InventarioMovimientoSalidas)
                        {
                            InventarioMovimientoDto movimientoDto = new InventarioMovimientoDto
                            {
                                IdProducto = item.IdProducto,
                                Cantidad = item.Cantidad,
                                Precio = item.Precio,
                                NumeroFactura = item.NumeroFactura,
                                Cufe = item.Cufe,
                                IdClienteProveedor = item.IdCliente,
                                Motivo = item.Motivo,
                                IdInventarioBodega = item.IdInventarioBodega,
                                Ip = item.Ip,
                                UsuarioCreacion = item.UsuarioCreacion,
                                FechaCreacion = item.FechaCreacion,
                                UsuarioModificacion = item.UsuarioModificacion,
                                FechaModificacion = item.FechaModificacion,
                                UsuarioEliminacion = item.UsuarioEliminacion,
                                FechaEliminacion = item.FechaEliminacion,
                                Estado = item.Estado,
                                IdInventarioMovimiento = item.IdInventarioMovimiento
                            };
                            movimientoDtos.Add(movimientoDto);
                        }
                    }
                }

                return movimientoDtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool InsUpdMovimientoEntrada(bool nuevo, InventarioMovimientoDto movimiento, ref string mensaje)
        {
            try
            {
                if (nuevo)
                {
                    InventarioMovimientoEntrada InventarioMovimientoEntrada = new InventarioMovimientoEntrada
                    {
                        IdProducto = movimiento.IdProducto,
                        Cantidad = movimiento.Cantidad,
                        Precio = movimiento.Precio,
                        NumeroFactura = movimiento.NumeroFactura,
                        Cufe = movimiento.Cufe,
                        IdProveedor = movimiento.IdClienteProveedor,
                        Motivo = movimiento.Motivo,
                        IdInventarioBodega = movimiento.IdInventarioBodega,
                        Ip = movimiento.Ip,
                        UsuarioCreacion = movimiento.UsuarioCreacion,
                        FechaCreacion = movimiento.FechaCreacion,
                        Estado = movimiento.Estado
                    };
                    _context.InventarioMovimientoEntrada.Add(InventarioMovimientoEntrada);
                    _context.SaveChanges();
                }
                else
                {
                    InventarioMovimientoEntrada InventarioMovimientoEntrada = new InventarioMovimientoEntrada
                    {
                        IdInventarioMovimiento = movimiento.IdInventarioMovimiento,
                        IdProducto = movimiento.IdProducto,
                        Cantidad = movimiento.Cantidad,
                        Precio = movimiento.Precio,
                        NumeroFactura = movimiento.NumeroFactura,
                        Cufe = movimiento.Cufe,
                        IdProveedor = movimiento.IdClienteProveedor,
                        Motivo = movimiento.Motivo,
                        IdInventarioBodega = movimiento.IdInventarioBodega,
                        Ip = movimiento.Ip,
                        UsuarioModificacion = movimiento.UsuarioModificacion,
                        FechaModificacion = movimiento.FechaModificacion,
                        Estado = movimiento.Estado
                    };

                    _context.InventarioMovimientoEntrada.Update(InventarioMovimientoEntrada);
                    _context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                return false;
            }
        }

        public bool InsUpdMovimientoSalida(bool nuevo, InventarioMovimientoDto movimiento)
        {
            try
            {
                if (nuevo)
                {

                }
                else
                {

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool QryInventarioMovimiento(InventarioMantenimientoDto mantenimientoDto, long IdUsuario, string IP, ref long IdInventarioMovimiento, ref string mensaje, ref string mensajeError)
        {
            var transaction = _context.Database.BeginTransaction();
            string codigo = DomainConstants.CODIGO_TRANSACCION;
            /*
             tabla:
            1.- Entrada
            2.- Salida
             */
            string codigoTransferencia = "";

            try
            {
                codigoTransferencia = codigo.Replace("{Fecha}", DateTime.Now.ToString("yyyyMMdd")).Replace("{Hora}", DateTime.Now.ToString("HHmmss"))
                                            .Replace("{Usuario}", IdUsuario.ToString()).Replace("{Compania}", "")
                                            .Replace("{Tipo_Inventario}", mantenimientoDto.tipoInventario.ToString())
                                            .Replace("{Tipo_Movimiento}", mantenimientoDto.tipoMovimiento.ToString());

                Producto Producto = _context.Producto.FirstOrDefault(x => x.ProductoId == mantenimientoDto.productos && x.Estado == true);
                if (Producto == null)
                {
                    mensaje = "Producto no encontrado";
                    transaction.Rollback();
                    return false;
                }

                Bodega Bodega = _context.Bodega.FirstOrDefault(x => x.BodegaId == mantenimientoDto.bodegas && x.Estado == true);
                if (Bodega == null)
                {
                    mensaje = "Bodega no encontrada";
                    transaction.Rollback();
                    return false;
                }

                if (mantenimientoDto.tipoInventario == 1) //Proveedor
                {
                    InventarioProveedor Inventario = _context.InventarioProveedor.FirstOrDefault(x => x.IdProducto == mantenimientoDto.productos && x.IdInventarioBodega == mantenimientoDto.bodegas && x.IdSucursal == mantenimientoDto.sucursal && x.Estado == true);
                    if (Inventario == null)
                    {
                        Inventario = new InventarioProveedor
                        {
                            IdProducto = mantenimientoDto.productos,
                            IdInventarioBodega = mantenimientoDto.bodegas,
                            StockActual = mantenimientoDto.tipoMovimiento == 1 ? mantenimientoDto.cantidad : -mantenimientoDto.cantidad,
                            UnidadMedida = mantenimientoDto.unidadMedida,
                            CantidadDescripcion = mantenimientoDto.cantidadDescripcion,
                            IdSucursal = mantenimientoDto.sucursal,
                            UsuarioCreacion = IdUsuario,
                            FechaCreacion = DateTime.Now,
                            Ip = IP,
                            Estado = true
                        };
                        _context.InventarioProveedor.Add(Inventario);
                    }
                    else
                    {
                        if (mantenimientoDto.tipoMovimiento == 1)
                        {
                            Inventario.StockActual += mantenimientoDto.cantidad;
                        }
                        else
                        {
                            Inventario.StockActual -= mantenimientoDto.cantidad;
                        }

                        if (!string.IsNullOrEmpty(mantenimientoDto.unidadMedida))
                        {
                            Inventario.UnidadMedida = mantenimientoDto.unidadMedida;
                        }

                        if (!string.IsNullOrEmpty(mantenimientoDto.cantidadDescripcion))
                        {
                            Inventario.CantidadDescripcion = mantenimientoDto.cantidadDescripcion;
                        }

                        Inventario.IdInventarioBodega = mantenimientoDto.bodegas;
                        Inventario.UsuarioModificacion = IdUsuario;
                        Inventario.FechaModificacion = DateTime.Now;
                        Inventario.Ip = IP;
                        _context.InventarioProveedor.Update(Inventario);
                    }
                    int cont = _context.SaveChanges();
                    if (cont <= 0)
                    {
                        mensaje = "No ha sido posible registrar la Entrada/Salida";
                        transaction.Rollback();
                        return false;
                    }
                    else
                    {
                        codigoTransferencia = codigoTransferencia.Replace("{Id_Inventario}", Inventario.IdInventarioProveedor.ToString());
                    }
                }
                else
                {
                    InventarioVenta Inventario = _context.InventarioVenta.FirstOrDefault(x => x.IdProducto == mantenimientoDto.productos && x.IdInventarioBodega == mantenimientoDto.bodegas && x.IdSucursal == mantenimientoDto.sucursal && x.Estado == true);
                    if (Inventario == null)
                    {
                        Inventario = new InventarioVenta();
                        Inventario.IdProducto = mantenimientoDto.productos;
                        Inventario.IdInventarioBodega = mantenimientoDto.bodegas;
                        Inventario.StockActual = mantenimientoDto.tipoMovimiento == 1 ? mantenimientoDto.cantidad : -mantenimientoDto.cantidad;
                        Inventario.UnidadMedida = mantenimientoDto.unidadMedida;
                        Inventario.CantidadDescripcion = mantenimientoDto.cantidadDescripcion;
                        Inventario.IdSucursal = mantenimientoDto.sucursal;
                        Inventario.UsuarioCreacion = IdUsuario;
                        Inventario.FechaCreacion = DateTime.Now;
                        Inventario.Ip = IP;
                        Inventario.Estado = true;
                        _context.InventarioVenta.Add(Inventario);
                    }
                    else
                    {
                        if (mantenimientoDto.tipoMovimiento == 1)
                        {
                            Inventario.StockActual += mantenimientoDto.cantidad;
                        }
                        else
                        {
                            Inventario.StockActual -= mantenimientoDto.cantidad;
                        }

                        if (!string.IsNullOrEmpty(mantenimientoDto.unidadMedida))
                        {
                            Inventario.UnidadMedida = mantenimientoDto.unidadMedida;
                        }

                        if (!string.IsNullOrEmpty(mantenimientoDto.cantidadDescripcion))
                        {
                            Inventario.CantidadDescripcion = mantenimientoDto.cantidadDescripcion;
                        }

                        Inventario.IdInventarioBodega = mantenimientoDto.bodegas;
                        Inventario.UsuarioModificacion = IdUsuario;
                        Inventario.FechaModificacion = DateTime.Now;
                        Inventario.Ip = IP;
                        _context.InventarioVenta.Update(Inventario);
                    }
                    int cont = _context.SaveChanges();
                    if (cont <= 0)
                    {
                        mensaje = "No ha sido posible registrar la Entrada/Salida";
                        transaction.Rollback();
                        return false;
                    }
                    else
                    {
                        codigoTransferencia = codigoTransferencia.Replace("{Id_Inventario}", Inventario.IdInventarioVenta.ToString());
                    }
                }

                if (mantenimientoDto.tipoMovimiento == 1)
                {
                    InventarioMovimientoEntrada movimiento = new InventarioMovimientoEntrada
                    {
                        Cantidad = mantenimientoDto.cantidad,
                        IdProducto = mantenimientoDto.productos,
                        IdProveedor = mantenimientoDto.proveedor,
                        IdSucursal = mantenimientoDto.sucursal,
                        Motivo = mantenimientoDto.motivo,
                        IdInventarioBodega = mantenimientoDto.bodegas,
                        NumeroFactura = mantenimientoDto.numeroFactura,
                        Cufe = mantenimientoDto.cufeFactura,
                        Precio = mantenimientoDto.precio,
                        UsuarioCreacion = IdUsuario,
                        FechaCreacion = DateTime.Now,
                        TipoInventario = mantenimientoDto.tipoInventario,
                        TipoMovimiento = mantenimientoDto.subTipoMovimiento,
                        CodigoTransferencia = codigoTransferencia.Replace("{Tabla}", "1"),
                        Ip = IP,
                        Estado = true
                    };
                    _context.InventarioMovimientoEntrada.Add(movimiento);
                    int con = _context.SaveChanges();
                    if (con <= 0)
                    {
                        mensaje = "No ha sido posible registrar la Entrada/Salida";
                        transaction.Rollback();
                        return false;
                    }
                    else
                    {
                        IdInventarioMovimiento = movimiento.IdInventarioMovimiento;
                    }
                }
                else
                {
                    InventarioMovimientoSalida movimiento = new InventarioMovimientoSalida
                    {
                        Cantidad = mantenimientoDto.cantidad,
                        IdProducto = mantenimientoDto.productos,
                        IdCliente = mantenimientoDto.cliente,
                        IdSucursal = mantenimientoDto.sucursal,
                        Motivo = mantenimientoDto.motivo,
                        IdInventarioBodega = mantenimientoDto.bodegas,
                        NumeroFactura = mantenimientoDto.numeroFactura,
                        Cufe = mantenimientoDto.cufeFactura,
                        Precio = mantenimientoDto.precio,
                        UsuarioCreacion = IdUsuario,
                        FechaCreacion = DateTime.Now,
                        TipoInventario = mantenimientoDto.tipoInventario,
                        TipoMovimiento = mantenimientoDto.subTipoMovimiento,
                        CodigoTransferencia = codigoTransferencia.Replace("{Tabla}", "2"),
                        Ip = IP,
                        Estado = true
                    };
                    _context.InventarioMovimientoSalida.Add(movimiento);
                    int con = _context.SaveChanges();
                    if (con <= 0)
                    {
                        mensaje = "No ha sido posible registrar la Entrada/Salida";
                        transaction.Rollback();
                        return false;
                    }
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                mensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                transaction.Rollback();
                return false;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public bool QryInventarioTransferencia(long IdUsuario, string IP, InventarioTransferenciaDto inventarioTransferenciaDto, ref string mensaje, ref string mensajeError)
        {
            var transaction = _context.Database.BeginTransaction();
            string codigo = DomainConstants.CODIGO_TRANSACCION;
            /*
             tabla:
            1.- Entrada
            2.- Salida
             */
            string codigoTransferenciaEntrada = "";
            string codigoTransferenciaSalida = "";
            try
            {
                int cont = 0;
                

                codigoTransferenciaEntrada = codigo.Replace("{Fecha}", DateTime.Now.ToString("yyyyMMdd")).Replace("{Hora}", DateTime.Now.ToString("HHmmss"))
                                            .Replace("{Usuario}", IdUsuario.ToString()).Replace("{Compania}", "")
                                            .Replace("{Tipo_Inventario}", inventarioTransferenciaDto.TipoInventarioOrigen.ToString())
                                            .Replace("{Tipo_Movimiento}", inventarioTransferenciaDto.TipoInventarioOrigen.ToString());

                codigoTransferenciaSalida = codigo.Replace("{Fecha}", DateTime.Now.ToString("yyyyMMdd")).Replace("{Hora}", DateTime.Now.ToString("HHmmss"))
                                            .Replace("{Usuario}", IdUsuario.ToString()).Replace("{Compania}", "")
                                            .Replace("{Tipo_Inventario}", inventarioTransferenciaDto.TipoInventarioDestino.ToString())
                                            .Replace("{Tipo_Movimiento}", inventarioTransferenciaDto.TipoInventarioDestino.ToString());

                Producto Producto = _context.Producto.FirstOrDefault(x => x.ProductoId == inventarioTransferenciaDto.Producto && x.Estado == true);
                if (Producto == null)
                {
                    mensaje = "Producto no encontrado";
                    transaction.Rollback();
                    return false;
                }

                Bodega BodegaOrigen = _context.Bodega.FirstOrDefault(x => x.BodegaId == inventarioTransferenciaDto.BodegaOrigen && x.Estado == true);
                if (BodegaOrigen == null)
                {
                    mensaje = "Bodega Origen no encontrada";
                    transaction.Rollback();
                    return false;
                }

                Bodega BodegaDestino = _context.Bodega.FirstOrDefault(x => x.BodegaId == inventarioTransferenciaDto.BodegaDestino && x.Estado == true);
                if (BodegaDestino == null)
                {
                    mensaje = "Bodega Destino no encontrada";
                    transaction.Rollback();
                    return false;
                }

                if (inventarioTransferenciaDto.TipoInventarioOrigen == (int)TipoInventario.proveedor)
                {
                    InventarioProveedor InventarioOrigen = _context.InventarioProveedor
                        .FirstOrDefault(x => x.IdProducto == inventarioTransferenciaDto.Producto && x.IdInventarioBodega == inventarioTransferenciaDto.BodegaOrigen && x.IdSucursal == inventarioTransferenciaDto.SucursalOrigen && x.Estado == true);
                    if (InventarioOrigen != null)
                    {
                        //Salida
                        InventarioOrigen.StockActual -= inventarioTransferenciaDto.Cantidad;
                        InventarioOrigen.IdInventarioBodega = inventarioTransferenciaDto.BodegaOrigen;
                        InventarioOrigen.UsuarioModificacion = IdUsuario;
                        InventarioOrigen.FechaModificacion = DateTime.Now;
                        InventarioOrigen.Ip = IP;

                        _context.InventarioProveedor.Update(InventarioOrigen);
                        cont = _context.SaveChanges();
                        if (cont <= 0)
                        {
                            mensaje = "No ha sido posible registrar la Entrada/Salida";
                            transaction.Rollback();
                            return false;
                        }
                        else
                        {
                            codigoTransferenciaSalida = codigoTransferenciaSalida.Replace("{Id_Inventario}", InventarioOrigen.IdInventarioProveedor.ToString());
                        }
                    }

                    if (inventarioTransferenciaDto.TipoInventarioDestino == (int)TipoInventario.proveedor)
                    {
                        InventarioProveedor InventarioDestino = _context.InventarioProveedor
                            .FirstOrDefault(x => x.IdProducto == inventarioTransferenciaDto.Producto && x.IdInventarioBodega == inventarioTransferenciaDto.BodegaDestino && x.IdSucursal == inventarioTransferenciaDto.SucursalDestino && x.Estado == true);
                        if (InventarioDestino == null)
                        {
                            //Entrada
                            InventarioDestino = new InventarioProveedor();
                            InventarioDestino.IdProducto = inventarioTransferenciaDto.Producto;
                            InventarioDestino.IdInventarioBodega = inventarioTransferenciaDto.BodegaDestino;
                            InventarioDestino.StockActual = inventarioTransferenciaDto.Cantidad;
                            InventarioDestino.IdSucursal = inventarioTransferenciaDto.SucursalDestino;
                            InventarioDestino.UsuarioCreacion = IdUsuario;
                            InventarioDestino.FechaCreacion = DateTime.Now;
                            InventarioDestino.Ip = IP;
                            InventarioDestino.Estado = true;

                            //InventarioDestino.IdInventarioProveedor = inventarioTransferenciaDto.Proveedor;

                            _context.InventarioProveedor.Add(InventarioDestino);
                        }
                        else
                        {
                            InventarioDestino.StockActual += inventarioTransferenciaDto.Cantidad;
                            InventarioDestino.IdInventarioBodega = inventarioTransferenciaDto.BodegaDestino;
                            InventarioDestino.UsuarioModificacion = IdUsuario;
                            InventarioDestino.FechaModificacion = DateTime.Now;
                            InventarioDestino.Ip = IP;
                            _context.InventarioProveedor.Update(InventarioDestino);
                        }

                        cont = _context.SaveChanges();
                        if (cont <= 0)
                        {
                            mensaje = "No ha sido posible registrar la Entrada/Salida";
                            transaction.Rollback();
                            return false;
                        }
                        else
                        {
                            codigoTransferenciaEntrada = codigoTransferenciaEntrada.Replace("{Id_Inventario}", InventarioDestino.IdInventarioProveedor.ToString());
                        }
                    }
                    else if (inventarioTransferenciaDto.TipoInventarioDestino == (int)TipoInventario.venta)
                    {
                        InventarioVenta InventarioDestino = _context.InventarioVenta
                            .FirstOrDefault(x => x.IdProducto == inventarioTransferenciaDto.Producto && x.IdInventarioBodega == inventarioTransferenciaDto.BodegaDestino && x.IdSucursal == inventarioTransferenciaDto.SucursalDestino && x.Estado == true);
                        if (InventarioDestino == null)
                        {
                            //Entrada
                            InventarioDestino = new InventarioVenta();
                            InventarioDestino.IdProducto = inventarioTransferenciaDto.Producto;
                            InventarioDestino.IdInventarioBodega = inventarioTransferenciaDto.BodegaDestino;
                            InventarioDestino.StockActual = inventarioTransferenciaDto.Cantidad;
                            InventarioDestino.IdSucursal = inventarioTransferenciaDto.SucursalDestino;
                            InventarioDestino.CantidadDescripcion = inventarioTransferenciaDto.CantidadDescripcion;
                            InventarioDestino.UnidadMedida = inventarioTransferenciaDto.UnidadMedida;

                            InventarioDestino.UsuarioCreacion = IdUsuario;
                            InventarioDestino.FechaCreacion = DateTime.Now;
                            InventarioDestino.Ip = IP;
                            InventarioDestino.Estado = true;

                            //InventarioDestino.IdInventarioProveedor = inventarioTransferenciaDto.Proveedor;

                            _context.InventarioVenta.Add(InventarioDestino);
                        }
                        else
                        {
                            InventarioDestino.StockActual += inventarioTransferenciaDto.Cantidad;
                            InventarioDestino.IdInventarioBodega = inventarioTransferenciaDto.BodegaDestino;
                            InventarioDestino.UsuarioModificacion = IdUsuario;
                            InventarioDestino.FechaModificacion = DateTime.Now;
                            InventarioDestino.Ip = IP;
                            _context.InventarioVenta.Update(InventarioDestino);
                        }

                        cont = _context.SaveChanges();
                        if (cont <= 0)
                        {
                            mensaje = "No ha sido posible registrar la Entrada/Salida";
                            transaction.Rollback();
                            return false;
                        }
                        else
                        {
                            codigoTransferenciaEntrada = codigoTransferenciaEntrada.Replace("{Id_Inventario}", InventarioDestino.IdInventarioVenta.ToString());
                        }
                    }
                }
                else if(inventarioTransferenciaDto.TipoInventarioOrigen == (int)TipoInventario.venta)
                {
                    InventarioVenta InventarioOrigen = _context.InventarioVenta
                        .FirstOrDefault(x => x.IdProducto == inventarioTransferenciaDto.Producto && x.IdInventarioBodega == inventarioTransferenciaDto.BodegaOrigen && x.IdSucursal == inventarioTransferenciaDto.SucursalOrigen && x.Estado == true);
                    if (InventarioOrigen != null)
                    {
                        //Salida
                        InventarioOrigen.StockActual -= inventarioTransferenciaDto.Cantidad;
                        InventarioOrigen.IdInventarioBodega = inventarioTransferenciaDto.BodegaOrigen;
                        InventarioOrigen.UsuarioModificacion = IdUsuario;
                        InventarioOrigen.FechaModificacion = DateTime.Now;
                        InventarioOrigen.Ip = IP;
                        _context.InventarioVenta.Update(InventarioOrigen);
                        cont = _context.SaveChanges();
                        if (cont <= 0)
                        {
                            mensaje = "No ha sido posible registrar la Entrada/Salida";
                            transaction.Rollback();
                            return false;
                        }
                        else
                        {
                            codigoTransferenciaSalida = codigoTransferenciaSalida.Replace("{Id_Inventario}", InventarioOrigen.IdInventarioVenta.ToString());
                        }
                    }

                    if (inventarioTransferenciaDto.TipoInventarioDestino == (int)TipoInventario.proveedor)
                    {
                        InventarioProveedor InventarioDestino = _context.InventarioProveedor
                            .FirstOrDefault(x => x.IdProducto == inventarioTransferenciaDto.Producto && x.IdInventarioBodega == inventarioTransferenciaDto.BodegaDestino && x.IdSucursal == inventarioTransferenciaDto.SucursalDestino && x.Estado == true);
                        if (InventarioDestino == null)
                        {
                            //Entrada
                            InventarioDestino = new InventarioProveedor
                            {
                                IdProducto = inventarioTransferenciaDto.Producto,
                                IdInventarioBodega = inventarioTransferenciaDto.BodegaDestino,
                                StockActual = inventarioTransferenciaDto.Cantidad,
                                IdSucursal = inventarioTransferenciaDto.SucursalDestino,
                                CantidadDescripcion = inventarioTransferenciaDto.CantidadDescripcion,
                                UnidadMedida = inventarioTransferenciaDto.UnidadMedida,

                                UsuarioCreacion = IdUsuario,
                                FechaCreacion = DateTime.Now,
                                Ip = IP,
                                Estado = true
                            };

                            //InventarioDestino.IdInventarioProveedor = inventarioTransferenciaDto.Proveedor;
                            _context.InventarioProveedor.Add(InventarioDestino);
                        }
                        else
                        {
                            InventarioDestino.StockActual += inventarioTransferenciaDto.Cantidad;
                            InventarioDestino.IdInventarioBodega = inventarioTransferenciaDto.BodegaDestino;
                            InventarioDestino.UsuarioModificacion = IdUsuario;
                            InventarioDestino.FechaModificacion = DateTime.Now;
                            InventarioDestino.Ip = IP;
                            _context.InventarioProveedor.Update(InventarioDestino);
                        }

                        cont = _context.SaveChanges();
                        if (cont <= 0)
                        {
                            mensaje = "No ha sido posible registrar la Entrada/Salida";
                            transaction.Rollback();
                            return false;
                        }
                        else
                        {
                            codigoTransferenciaEntrada = codigoTransferenciaEntrada.Replace("{Id_Inventario}", InventarioDestino.IdInventarioProveedor.ToString());
                        }
                    }
                    else if (inventarioTransferenciaDto.TipoInventarioDestino == (int)TipoInventario.venta)
                    {
                        InventarioVenta InventarioDestino = _context.InventarioVenta
                            .FirstOrDefault(x => x.IdProducto == inventarioTransferenciaDto.Producto && x.IdInventarioBodega == inventarioTransferenciaDto.BodegaDestino && x.IdSucursal == inventarioTransferenciaDto.SucursalDestino && x.Estado == true);
                        if (InventarioDestino == null)
                        {
                            //Entrada
                            InventarioDestino = new InventarioVenta
                            {
                                IdProducto = inventarioTransferenciaDto.Producto,
                                IdInventarioBodega = inventarioTransferenciaDto.BodegaDestino,
                                StockActual = inventarioTransferenciaDto.Cantidad,
                                IdSucursal = inventarioTransferenciaDto.SucursalDestino,
                                UsuarioCreacion = IdUsuario,
                                FechaCreacion = DateTime.Now,
                                Ip = IP,
                                Estado = true
                            };

                            //InventarioDestino.IdInventarioProveedor = inventarioTransferenciaDto.Proveedor;

                            _context.InventarioVenta.Add(InventarioDestino);
                        }
                        else
                        {
                            InventarioDestino.StockActual += inventarioTransferenciaDto.Cantidad;
                            InventarioDestino.IdInventarioBodega = inventarioTransferenciaDto.BodegaDestino;
                            InventarioDestino.UsuarioModificacion = IdUsuario;
                            InventarioDestino.FechaModificacion = DateTime.Now;
                            InventarioDestino.Ip = IP;
                            _context.InventarioVenta.Update(InventarioDestino);
                        }

                        cont = _context.SaveChanges();
                        if (cont <= 0)
                        {
                            mensaje = "No ha sido posible registrar la Entrada/Salida";
                            transaction.Rollback();
                            return false;
                        }
                        else
                        {
                            codigoTransferenciaEntrada = codigoTransferenciaEntrada.Replace("{Id_Inventario}", InventarioDestino.IdInventarioVenta.ToString());
                        }
                    }
                }

                InventarioMovimientoEntrada movimientoEnt = new InventarioMovimientoEntrada
                {
                    Cantidad = inventarioTransferenciaDto.Cantidad,
                    IdProducto = inventarioTransferenciaDto.Producto,
                    IdProveedor = inventarioTransferenciaDto.Proveedor,
                    IdSucursal = inventarioTransferenciaDto.SucursalDestino,
                    Motivo = inventarioTransferenciaDto.Motivo,
                    IdInventarioBodega = inventarioTransferenciaDto.BodegaDestino,
                    NumeroFactura = inventarioTransferenciaDto.NumeroFactura,
                    Cufe = inventarioTransferenciaDto.Cufe,
                    Precio = inventarioTransferenciaDto.Precio,
                    CodigoTransferencia = codigoTransferenciaEntrada.Replace("{Tabla}", "1"),
                    UsuarioCreacion = IdUsuario,
                    FechaCreacion = DateTime.Now,
                    TipoInventario = inventarioTransferenciaDto.TipoInventarioDestino,
                    TipoMovimiento = inventarioTransferenciaDto.SubTipoMovimiento,
                    Ip = IP,
                    Estado = true
                };
                _context.InventarioMovimientoEntrada.Add(movimientoEnt);
                cont = _context.SaveChanges();
                if (cont <= 0)
                {
                    mensaje = "No ha sido posible registrar la Entrada/Salida";
                    transaction.Rollback();
                    return false;
                }

                InventarioMovimientoSalida movimientoSal = new InventarioMovimientoSalida
                {
                    Cantidad = inventarioTransferenciaDto.Cantidad,
                    IdProducto = inventarioTransferenciaDto.Producto,
                    IdCliente = inventarioTransferenciaDto.Cliente,
                    IdSucursal = inventarioTransferenciaDto.SucursalOrigen,
                    Motivo = inventarioTransferenciaDto.Motivo,
                    IdInventarioBodega = inventarioTransferenciaDto.BodegaOrigen,
                    NumeroFactura = inventarioTransferenciaDto.NumeroFactura,
                    Cufe = inventarioTransferenciaDto.Cufe,
                    Precio = inventarioTransferenciaDto.Precio,
                    CodigoTransferencia = codigoTransferenciaSalida.Replace("{Tabla}", "2"),
                    UsuarioCreacion = IdUsuario,
                    FechaCreacion = DateTime.Now,
                    TipoInventario = inventarioTransferenciaDto.TipoInventarioOrigen,
                    TipoMovimiento = inventarioTransferenciaDto.SubTipoMovimiento,
                    Ip = IP,
                    Estado = true
                };
                _context.InventarioMovimientoSalida.Add(movimientoSal);
                cont = _context.SaveChanges();
                if (cont <= 0)
                {
                    mensaje = "No ha sido posible registrar la Entrada/Salida";
                    transaction.Rollback();
                    return false;
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                mensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                transaction.Rollback();
                return false;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public List<ProductoBodegaDto> SelProductosBodega(long IdBodega, int Sucursal, int TipoInventario)
        {
            List<ProductoBodegaDto> productosDto = new List<ProductoBodegaDto>();
            try
            {
                if (TipoInventario == (int)FRIO.MAR.APPLICATION.CORE.Constants.TipoInventario.proveedor)
                {
                    List<InventarioProveedor> inventario = _context.InventarioProveedor
                        .Where(x => x.IdInventarioBodega == IdBodega && x.IdSucursal == Sucursal && x.Estado == true)
                        .Include(x => x.IdProductoNavigation)
                        .ToList();
                    if (inventario == null || !inventario.Any())
                    {
                        return new List<ProductoBodegaDto>();
                    }

                    foreach (var item in inventario)
                    {
                        //var props = prod.FirstOrDefault(x => x.IdProducto == item.IdProducto);
                        ProductoBodegaDto productoBodegaDto = new ProductoBodegaDto();
                        productoBodegaDto.IdProducto = item.IdProductoNavigation.ProductoId;
                        productoBodegaDto.Codigo = item.IdProductoNavigation.Codigo;
                        productoBodegaDto.Descripcion = item.IdProductoNavigation.Descripcion;
                        productoBodegaDto.Stock = item.StockActual == null ? "0" : Utilidades.DoubleToString_FrontCO((decimal)item?.StockActual, 2); 
                        productosDto.Add(productoBodegaDto);
                    }
                }
                else if (TipoInventario == (int)FRIO.MAR.APPLICATION.CORE.Constants.TipoInventario.venta)
                {
                    List<InventarioVenta> inventario = _context.InventarioVenta
                        .Where(x => x.IdInventarioBodega == IdBodega && x.IdSucursal == Sucursal && x.Estado == true)
                        .Include(x => x.IdProductoNavigation)
                        .ToList();
                    if (inventario == null || !inventario.Any())
                    {
                        return new List<ProductoBodegaDto>();
                    }

                    foreach (var item in inventario)
                    {
                        //var props = prod.FirstOrDefault(x => x.IdProducto == item.IdProducto);
                        ProductoBodegaDto productoBodegaDto = new ProductoBodegaDto();
                        productoBodegaDto.IdProducto = item.IdProductoNavigation.ProductoId;
                        productoBodegaDto.Codigo = item.IdProductoNavigation.Codigo;
                        productoBodegaDto.Descripcion = item.IdProductoNavigation.Descripcion;
                        productoBodegaDto.Stock = item.StockActual == null ? "0" : Utilidades.DoubleToString_FrontCO((decimal)item?.StockActual, 0);
                        productosDto.Add(productoBodegaDto);
                    }
                }

                return productosDto;
            }
            catch (Exception ex)
            {
                return new List<ProductoBodegaDto>();
            }
        }

        public List<Producto> SelProductos(int cantidad, long IdProducto, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return _context.Producto
                    .Where(x => x.Estado == true)
                    .Where(x => x.ProductoId == IdProducto || IdProducto == 0)
                    .Where(x => x.FechaCreacion >= fechaInicio && x.FechaCreacion <= fechaFin)
                    .Take(cantidad)
                    .ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Bodega> GetBodegas(long IdCompania)
        {
            return _context.Bodega.Where(x => x.Estado == true).ToList();
        }
    }
}
