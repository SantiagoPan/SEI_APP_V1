import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  CButton,
  CCard,
  CCardText,
  CCardBody,
  cilCart,
  CCol,
  CContainer,
  CTable,
  CTableBody,
  CTableHead,
  CTableRow,
  CTableDataCell,
  CTableHeaderCell,
  CTooltip,
  CCardTitle,
  CForm,
  CFormInput,
  CModal,
  CFormCheck,
  CFormLabel,
  CModalBody,
  CAlert,
  CModalHeader,
  CModalFooter,
  CModalTitle, CFormSelect,
  CCardSubtitle,
  CRow,
  CFormTextarea,
  cilMagnifyingGlass,


} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilSearch, cilZoom, cilUser, cilEye, cilTrash, cilToggleOff, cilToggleOn } from '@coreui/icons'

function MyPosts(props) {
  const [PostsServices, setPostsServices] = useState([]);
  const [PostsProducts, setPostsProducts] = useState([]);
  const [modalProduct, setModalProduct] = useState([]);
  const [modalService, setModalService] = useState([]);
  const [visibleDetProduct, setVisibleDetProduct] = useState(false)
  const [visibleDetService, setVisibleDetService] = useState(false)


  const GetPostsServices = async () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    axios
      .get("https://localhost:44342/services/getServicesPostedByUser?idUser=" + infoUser.idUser)
      .then(response => {
        setPostsServices(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  const GetPostsProducts = async () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    axios
      .get("https://localhost:44342/product/getProductsPostedByUser?idUser=" + infoUser.idUser)
      .then(response => {
        setPostsProducts(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  useEffect(() => {
    GetPostsServices();
    GetPostsProducts();
  }, [])

  const showModalProductDetails = (idProducto) => {
    let ProductDetailsModal = PostsProducts.filter(v => v.producto.idProducto == idProducto);
    setModalProduct(ProductDetailsModal)
    setVisibleDetProduct(!visibleDetProduct)
  };

  const showModalServiceDetails = (idServicio) => {
    console.log(idServicio);
    let ServiceDetailsModal = PostsServices.filter(v => v.servicio.idServicio == idServicio);
    setModalService(ServiceDetailsModal)
    setVisibleDetService(!visibleDetService)
  };

  const disableProduct = (idProducto, idEstado) => {
    console.log("Actualizar a eliminar =", idProducto);
    axios
      .get("https://localhost:44342/product/updateStatusProduct?idProducto=" + idProducto + "&idEstadoProducto=" + idEstado)
      .then(response => {
        window.location.reload(true);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const disableService = (idServicio,idEstado) => {
    console.log("Actualizar a eliminar =", idServicio);
    axios
      .get("https://localhost:44342/services/updateStatusService?idServicio=" + idServicio + "&idEstado=" + idEstado)
      .then(response => {
        window.location.reload(true);
      })
      .catch((error) => {
        console.log(error);
      });
  };
  
  let modalInfoProduct = modalProduct.length > 0
    && modalProduct.map((producto, i) => {
      return (
        <CModal visible={visibleDetProduct} onClose={() => setVisibleDetProduct(false)}>
          <CModalHeader onClose={() => setVisibleDetProduct(false)}>
            <CModalTitle>Detalles del producto - {producto.producto.nombreProducto}</CModalTitle>
          </CModalHeader>
          <CCard className="mx-4" key={producto.producto.idProducto}>
            <CCardBody className="p-4">
              <CForm className="user">
                <CRow className="mb-3">
                  <CCardSubtitle><strong>Descripción</strong> </CCardSubtitle>
                  <CCardText>{producto.producto.descripcion}</CCardText>
                  <CCardText><strong>Condición: </strong>{producto.producto.caracteristicas.condicion}</CCardText>
                  <CCardText><strong>Categoria: </strong>{producto.producto.nombreTipoProducto}</CCardText>
                  <CCardText><strong>Marca: </strong>{producto.producto.caracteristicas.marca}<strong> Modelo: </strong>{producto.producto.caracteristicas.modelo}</CCardText>
                  <CCardText><strong>Material: </strong>{producto.producto.caracteristicas.material}</CCardText>
                  <CCardText><strong>Dimenciones: </strong>Alto {producto.producto.caracteristicas.alto} cm x {producto.producto.caracteristicas.ancho} cm de ancho</CCardText>
                  <CCardText><strong>Garantia: </strong>{producto.producto.caracteristicas.garantia}</CCardText>
                  <CCardText><strong>Ubicación: </strong>{producto.producto.localizacion.ciudad}, {producto.producto.localizacion.departmento} <b> Barrio: </b> {producto.producto.localizacion.barrio}</CCardText>
                </CRow>
              </CForm>
            </CCardBody>
          </CCard>
          <CModalFooter>
          </CModalFooter>
        </CModal>
      )
    }, this);

  let modalInfoService = modalService.length > 0
    && modalService.map((item, i) => {
      return (
        <CModal visible={visibleDetService} onClose={() => setVisibleDetService(false)}>
          <CModalHeader onClose={() => setVisibleDetService(false)}>
            <CModalTitle>Datos del servicio - {item.servicio.nombreServicio}</CModalTitle>
          </CModalHeader>
          <CCard className="mx-4">
            <CCardBody className="p-4">
                <CRow className="mb-3">
                  <CCardSubtitle><strong>Descripción</strong> </CCardSubtitle>
                  <CCardText>{item.servicio.descripcion}</CCardText>
                  <CCardText><strong>Calificación: </strong>{item.servicio.calificacion}</CCardText>
                  <CCardText><strong>Categoría: </strong>{item.servicio.tipoServicio}</CCardText>
                  <CCardText><strong>Experiencia: </strong>{item.servicio.caracteristicas.experiencia}</CCardText>
                  <CCardText><strong>Incluye: </strong>{item.servicio.caracteristicas.incluye}</CCardText>
                  <CCardText><strong>No Incluye: </strong>{item.servicio.caracteristicas.noIncluye}</CCardText>
                  <CAlert color="info">Datos Del Prestador</CAlert>
                  <CCardText><strong>Nombre: </strong>{item.servicio.caracteristicas.nombre}</CCardText>
                  <CCardText><strong>Teléfono: </strong>{item.servicio.caracteristicas.telefono}</CCardText>
                  <CCardText><strong>Ubicación: </strong>{item.servicio.localizacion.ciudad}, {item.servicio.localizacion.departmento} <b> Barrio: </b> {item.servicio.localizacion.barrio}</CCardText>
                  {item.servicio.aplicaConvenio == "Si" ? (
                    <CCardText><strong>Tipo de pago: </strong> A convenir con el prestador</CCardText>
                  ) : (
                    <CCardText><strong>Precio: </strong>{item.servicio.costoServicio}</CCardText>
                  )}
                </CRow>
            </CCardBody>
          </CCard>
          <CModalFooter>
          </CModalFooter>
        </CModal>
      )
    }, this);

  return (
    <div className="bg-light min-vh-10 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={9} lg={7} xl={12}>
            <CCard className="mx-4">
              <CCardBody className="p-4">
                <h2>Mis Publicaciones</h2>
              </CCardBody>
              <CContainer>
                <div className="justify-content-center">
                  <h2>Productos</h2>
                <CTable bordered>
                  <CTableHead>
                    <CTableRow>
                        <CTableHeaderCell scope="col">Nombre</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Precio</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Fecha Publicacíón</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Estado</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Acciónes</CTableHeaderCell>
                    </CTableRow>
                  </CTableHead>
                  <CTableBody>
                      {PostsProducts &&
                        PostsProducts.map((postProduct) => (
                        <CTableRow>
                            <CTableDataCell>{postProduct.producto.nombreProducto}</CTableDataCell>
                            <CTableDataCell>{postProduct.producto.costoProducto}</CTableDataCell>
                            <CTableDataCell>{postProduct.producto.fechaPublicacion}</CTableDataCell>
                            <CTableDataCell>{postProduct.producto.estadoPublicacion}</CTableDataCell>
                            {postProduct.producto.estadoPublicacion == "ACTIVO" ? (
                            <CTableDataCell>
                                <CTooltip content="Ver Detalles" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => showModalProductDetails(postProduct.producto.idProducto)}><CIcon icon={cilZoom} /></CButton></CTooltip>
                                  | <CTooltip content="Desactivar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => disableProduct(postProduct.producto.idProducto,0)}><CIcon icon={cilToggleOn} /></CButton></CTooltip>
                                | <CTooltip content="Eliminar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => disableProduct(postProduct.producto.idProducto,3)}><CIcon icon={cilTrash}/></CButton></CTooltip>
                             </CTableDataCell>
                          ) : (
                              <CTableDataCell>
                                  <CTooltip content="Ver Detalles" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => showModalProductDetails(postProduct.producto.idProducto)}><CIcon icon={cilZoom} /></CButton></CTooltip>
                                  | <CTooltip content="Activar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => disableProduct(postProduct.producto.idProducto,0)}><CIcon icon={cilToggleOff} /></CButton></CTooltip>
                                  | <CTooltip content="Eliminar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => disableProduct(postProduct.producto.idProducto,3)}><CIcon icon={cilTrash}/></CButton></CTooltip>
                             </CTableDataCell>
                          )}
                        </CTableRow>
                        ))}
                  </CTableBody>
                </CTable>
                </div>
                <div>
                  {modalInfoProduct}
                </div>
                <br></br>
                <div className="justify-content-center">
                  <h2>Servicios</h2>
                  <CTable bordered>
                    <CTableHead>
                      <CTableRow>
                        <CTableHeaderCell scope="col">Nombre</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Precio</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Fecha Publicacíón</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Estado</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Acciónes</CTableHeaderCell>
                      </CTableRow>
                    </CTableHead>
                    <CTableBody>
                      {PostsServices &&
                        PostsServices.map((postService) => (
                          <CTableRow>
                            <CTableDataCell>{postService.servicio.nombreServicio}</CTableDataCell>
                            {postService.servicio.aplicaConvenio == "Si" ? (
                              <CTableDataCell>A convenir</CTableDataCell>
                            ) : (
                                <CTableDataCell>{postService.servicio.costoServicio}</CTableDataCell>)}
                            <CTableDataCell>{postService.servicio.fechaPublicacion}</CTableDataCell>
                            <CTableDataCell>{postService.servicio.estadoPublicacion}</CTableDataCell>
                            {postService.servicio.estadoPublicacion == "ACTIVO" ? (
                              <CTableDataCell>
                                <CTooltip content="Ver Detalles" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => showModalServiceDetails(postService.servicio.idServicio)}><CIcon icon={cilZoom} /></CButton></CTooltip>
                                | <CTooltip content="Desactivar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => disableService(postService.servicio.idServicio,0)}><CIcon icon={cilToggleOn} /></CButton></CTooltip>
                                | <CTooltip content="Eliminar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => disableService(postService.servicio.idServicio,3)}><CIcon icon={cilTrash} /></CButton></CTooltip>
                              </CTableDataCell>
                            ) : (
                              <CTableDataCell>
                                  <CTooltip content="Ver Detalles" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => showModalServiceDetails(postService.servicio.idServicio)}><CIcon icon={cilZoom} /></CButton></CTooltip>
                                  | <CTooltip content="Activar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => disableService(postService.servicio.idServicio,0)}><CIcon icon={cilToggleOff} /></CButton></CTooltip>
                                  | <CTooltip content="Eliminar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => disableService(postService.servicio.idServicio,3)}><CIcon icon={cilTrash}/></CButton></CTooltip>
                              </CTableDataCell>
                            )}
                          </CTableRow>
                        ))}
                    </CTableBody>
                  </CTable>
                </div>
                <div>
                  {modalInfoService}
                </div>
                </CContainer>
            </CCard>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default MyPosts
