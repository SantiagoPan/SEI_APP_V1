import axios from 'axios';
import React, { useState, useEffect } from 'react';
import {
  CCard,
  CCardBody,
  CCol,
  CContainer,
  CRow,
  CButton,
  CTooltip,
  CCardImage,
  CCardTitle,
  CFormSelect,
  CFormInput,
  CTable,
  CTableBody,
  CTableHead,
  CTableRow,
  CTableDataCell,
  CTableHeaderCell,
  CInputGroup,
  CAlert,
  CForm,
  CCardText,
  CFormLabel,
  CFormTextarea,
  CModal,
  CModalBody,
  CModalHeader,
  CModalFooter,
  CModalTitle,
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilTrash, cilSwapHorizontal, cilThumbUp, cilZoom, cilCloudDownload } from '@coreui/icons'

const MyOrders = () => {
  const [motivoFinalizacion, setMotivoFinalizacion] = useState([]);
  const [products, setDataProducts] = useState([]);
  const [productQualify, setProductModal] = useState(false);
  const [productsDetail, setProductModalDetail] = useState([]);
  const [services, setDataServices] = useState([]);
  const [serviceHire, setDataServiceHire] = useState([]);
  const [visibleDet, setVisibleDet] = useState(false);
  const [visibleQualify, setVisibleQualify] = useState(false);
  const [visibleHire, setVisibleHire] = useState(false);
  const [visibleFinish, setVisibleFinish] = useState(false);
  const [image, setImage] = useState("");
  const [url, setUrl] = useState("");
  const [messages, setMessages] = useState({
    Mensaje: ''
  });
  const [calificacion, setDataCalificacion] = useState({
    IdUsuario: '',
    IdProducto: '',
    Observacion: '',
    Calificacion: ''
  });

  const [finalizacionServicio, setFinalizacionServicio] = useState({
    IdUsuario: '',
    IdServicio: 0,
    IdVentaServicio: 0,
    Observacion: '',
    Calificacion: ''
  });

  const GetProductos = async () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    axios
      .get("https://localhost:44342/product/getProductsByUser?idUser=" + infoUser.idUser)
      .then(response => {
        setDataProducts(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
  }

  const GetServicios = async () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    axios
      .get("https://localhost:44342/services/getServicesByUser?idUser=" + infoUser.idUser)
      .then(response => {
        setDataServices(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
  }

  const GetMotivoFinalizacion = async () => {
    axios
      .get("https://localhost:44342/services/getMotivoFinalizacion")
      .then(response => {
        setMotivoFinalizacion(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
  }

  const QualifyProduct = (e) => {
    e.preventDefault();
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    const data = {
      IdUsuario: infoUser.idUser,
      IdProducto: calificacion.IdProducto,
      Observacion: calificacion.Observacion,
      Calificacion: calificacion.Calificacion
    };
    const apiUrl = "https://localhost:44342/product/qualifyProduct";
    axios.post(apiUrl, data)
      .then((result) => {
        console.log(result.data);
        const serializedState = JSON.stringify(result.data.UserDetails);
        var a = localStorage.setItem('BuyProducts', serializedState);
        console.log("A:", a)
        const user = result.data.token;
        console.log(user);
        if (result.status == 200)
          window.location.reload(true);
        else
          alert('No registrado');
      })
  };

  const FinishService = () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    const data = {
      IdUsuarioComprador: infoUser.idUser,
      IdServicio: finalizacionServicio.IdServicio,
      IdMotivoFinalizacion: finalizacionServicio.IdMotivoFinalizacion,
      IdVentaServicio: finalizacionServicio.IdVentaServicio,
      Calificacion: finalizacionServicio.Calificacion,
      Observacion: finalizacionServicio.Observacion
    };
    const apiUrl = "https://localhost:44342/services/finishService";
    axios.post(apiUrl, data)
      .then((result) => {
        console.log(result.data);
        const serializedState = JSON.stringify(result.data.UserDetails);
        var a = localStorage.setItem('FinishService', serializedState);
        console.log("A:", a)
        const user = result.data.token;
        console.log(user);
        if (result.status == 200)
          window.location.reload(true);
        else
          alert('No registrado');
      })
  };

  const handleDownload = (url, filename) => {
    console.log("url ", url);
    console.log("filename ", filename);
    axios
      .get(url, {
        responseType: "blob"
      })
      .then((res) => {
        fileDownload(res.data, filename);
      });
  };
  const UploadFiles = (e) => {
    var counFiles = image.length;
    var arrNames = [];
    var arrUrls = [];
    for (var i = 0; i < counFiles; i++) {
        var fileInfo = image[i];
        const dataImage = new FormData()
        dataImage.append("file", fileInfo)
        dataImage.append("upload_preset", "xx3mfqax")
        dataImage.append("cloud_name", "ddsbfi1l5")
        fetch("https://api.cloudinary.com/v1_1/ddsbfi1l5/image/upload", {
          method: "post",
          body: dataImage
        }).then(resp => resp.json())
          .then(data => {
            console.log(data);
            setUrl(data.url);
            var infoUser = JSON.parse(localStorage.getItem('myData'));
            const dataP = {
              IdVentaServicio: e,
              IdUsuario: infoUser.idUser,
              fileName: data.original_filename + "." + data.format,
              url: data.url
            };
            const apiUrl = "https://localhost:44342/services/uploadAttachments";
            axios.post(apiUrl, dataP)
              .then((result) => {
                console.log(result.data);
                if (result.status == 200)
                  window.location.reload(true);
                else
                  alert('No registrado');
              })
          }).catch(err => console.log(err))
    }
  };

  const SendMessage = (idVentaServicio) => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    const apiUrl = "https://localhost:44342/services/SendMessage";
    const data = {
      IdUsuario: infoUser.idUser,
      Mensaje: messages.Mensaje,
      IdVentaServicio: idVentaServicio
    };
    axios.post(apiUrl, data)
      .then((result) => {
        console.log(result.data);
        if (result.status == 200)
          window.location.reload(true);
        else
          alert("Información bancaria no registrada");
      })
  };


  let motivoFinalizacionList = motivoFinalizacion.length > 0
    && motivoFinalizacion.map((item, i) => {
      return (
        <option key={i} value={item.idMotivoFinalizacionServicio}>{item.nombre}</option>
      )
    }, this);



  const showModalFinish = () => {
    setVisibleFinish(!visibleFinish)
  };

  const hideModalHire = () => {
    setVisibleHire(false)
    setVisibleFinish(!visibleFinish)
  };

  const onChange = (e) => {
    e.persist();
    setDataCalificacion({ ...calificacion, [e.target.name]: e.target.value });
  }

  const onChangeFinish = (e) => {
    e.persist();
    setFinalizacionServicio({ ...finalizacionServicio, [e.target.name]: e.target.value });
  }

  const onChangeMessage = (e) => {
    e.persist();
    setMessages({ ...messages, [e.target.name]: e.target.value });
  }

  const showProductQualify = (idProducto) => {
    let productModal = products.filter(v => v.producto.idProducto == idProducto);
    setProductModal(productModal);
    setVisibleQualify(!visibleQualify);
  };

  const showProductDetails = (idProducto) => {
    let productModal = products.filter(v => v.producto.idProducto == idProducto);
    setProductModalDetail(productModal);
    setVisibleDet(!visibleDet)
  };

  const showServiceHire = (idServicio) => {
    let serviceModal = services.filter(v => v.servicio.idServicio == idServicio);
    console.log(serviceModal);
    setDataServiceHire(serviceModal);
    setVisibleHire(!visibleHire);
  };

  useEffect(() => {
    GetProductos();
    GetServicios();
    GetMotivoFinalizacion();
  }, [])

  let modalProduct = productQualify.length > 0
    && productQualify.map((producto, i) => {
      return (
        <CModal visible={visibleQualify} onClose={() => setVisibleQualify(false)}>
          <CModalHeader onClose={() => setVisibleQualify(false)}>
            <CModalTitle>Detalles del producto - {producto.producto.nombreProducto} | {producto.producto.condicion}</CModalTitle>
          </CModalHeader>
          <CCard className="mx-6" key={producto.producto.idProducto}>
            <CCardBody className="p-6">
              <CForm onSubmit={QualifyProduct} className="row g-3">
                <CFormInput type="hidden" value={calificacion.IdProducto = producto.producto.idProducto} className="form-control form-control-sm" name="IdProducto" id="IdProducto" />
                <CCol md={10}>
                  <CFormLabel htmlFor="Calificacion" className="col-sm-10 col-form-label">Calificación</CFormLabel>
                  <CFormSelect value={calificacion.Calificacion} onChange={onChange} aria-label="Default select example" name="Calificacion" id="Calificacion" label="Calificación">
                    <option> Seleccione una opción... </option>
                    <option value="1">★</option>
                    <option value="2">★ ★</option>
                    <option value="3">★ ★ ★</option>
                    <option value="4">★ ★ ★ ★</option>
                    <option value="5">★ ★ ★ ★ ★</option>
                  </CFormSelect>
                </CCol>
                <CCol md={10}>
                  <CFormLabel htmlFor="Observacion" className="col-sm-10 col-form-label">Observaciones</CFormLabel>
                  <CFormTextarea value={calificacion.Observacion} onChange={onChange} rows="3" name="Observacion" id="Observacion"></CFormTextarea>
                </CCol>
                <CCol md={6}>
                  <CButton type="submit" color="success">Calificar</CButton>
                </CCol>
              </CForm>
            </CCardBody>
          </CCard>
          <CModalFooter>
          </CModalFooter>
        </CModal>
      )
    }, this);

  let modalProductDetail = productsDetail.length > 0
    && productsDetail.map((producto, i) => {
      return (
        <CModal visible={visibleDet} onClose={() => setVisibleDet(false)}>
          <CModalHeader onClose={() => setVisibleDet(false)}>
            <CModalTitle>Detalles del producto - {producto.producto.nombreProducto} | {producto.producto.condicion}</CModalTitle>
          </CModalHeader>
          <CCard className="mx-4" key={producto.producto.idProducto}>
            <CCardBody className="p-4">
              <CForm className="user">
                <CRow className="mb-3">
                  <CCardText><strong>Marca: </strong>{producto.producto.marca}</CCardText>
                  <CCardText><strong>Unidades Compradas: </strong>{producto.producto.unidades}</CCardText>
                  <CCardText><strong>Fecha De Compra: </strong>{producto.producto.fechaCompra}</CCardText>
                  <CCardText><strong>Nombre Del Vendedor: </strong>{producto.producto.nombreVendedor}</CCardText>
                  <CCardText><strong>Tipo De Pago: </strong>{producto.producto.tipoPago}</CCardText>
                  <CCardText><strong>Valor Total:  </strong>{producto.producto.valorTotalCompra}</CCardText>
                </CRow>
              </CForm>
            </CCardBody>
          </CCard>
          <CModalFooter>
          </CModalFooter>
        </CModal>
      )
    }, this);

  let modalServiceDetail = serviceHire.length > 0
    && serviceHire.map((servicio, i) => {
      return (
        <CModal md={9} lg={7} xl={10} visible={visibleHire} onClose={() => setVisibleHire(false)}>
          <CModalHeader onClose={() => setVisibleHire(false)}>
            <CModalTitle>Contratar Servicio - {servicio.servicio.nombreServicio} </CModalTitle>
          </CModalHeader>
          <CModalBody>
            <CContainer>
            <CRow className="mb-3">
              <CAlert color="info">Datos Del Prestador</CAlert>
              <br></br>
              <CCardText><strong>Nombre Del Prestador: </strong> {servicio.servicio.nombrePrestador}</CCardText>
              <CCardText><strong>Experiencia: </strong> {servicio.servicio.experiencia}</CCardText>
              <CCardText><strong>Teléfono: </strong> {servicio.servicio.telefono}</CCardText>
              {servicio.servicio.numeroCuenta == null ? (<CCardText > <strong>Cuenta Bancaria: </strong>Sin informar</CCardText>) : (<CCardText><strong>Cuenta Bancaria: </strong> {servicio.servicio.numeroCuenta - servicio.servicio.tipoCuenta, servicio.servicio.nombreBanco} </CCardText>)}
            </CRow>
            <CRow className="mb-3">
              <CAlert color="info">Documentos del servicio</CAlert>
              <br></br>
              <CInputGroup className="mb-3">
                <CFormInput type="file" onChange={(e) => setImage(e.target.files)} id="Documentos" name="Documentos" multiple />
                <CButton color="secondary" onClick={() => UploadFiles(servicio.servicio.idVentaServicio)}>Subir Archivos</CButton>
              </CInputGroup>

              {servicio.servicio.adjuntos.length == 0 ? (
                <CCardText><strong>Nos se han subido archivos: </strong></CCardText>
              ) : (
                <CTable bordered>
                  <CTableHead>
                    <CTableRow>
                      <CTableHeaderCell scope="col">Nombre Del Documento</CTableHeaderCell>
                    </CTableRow>
                  </CTableHead>
                  <CTableBody>
                    {servicio.servicio.adjuntos &&
                      servicio.servicio.adjuntos.map((adjunto) => (
                        <CTableRow>
                          <CTableDataCell>{adjunto}</CTableDataCell>
                        </CTableRow>
                      ))}
                  </CTableBody>
                </CTable>
              )}
            </CRow>
            <CRow className="mb-3">
              <CAlert color="info">Contactar Al Prestador</CAlert>
              <br></br>
              <i>Enviale un mensaje al prestador si tienes alguna duda.</i>
              <CForm onSubmit={SendMessage}>
                <CInputGroup className="mb-3">
                  <CFormTextarea type="text" value={messages.Mensaje} onChange={onChangeMessage} className="form-control form-control-sm" id="Mensaje" name="Mensaje" rows="2" placeholder="Escribe aqui tu mensaje" ></CFormTextarea>
                </CInputGroup>
                <CButton color="success" onClick={() => SendMessage(servicio.servicio.idVentaServicio)}>Enviar mensaje</CButton>
              </CForm>
              </CRow>
              </CContainer>
          </CModalBody>
          <CModalFooter>
            <CCol md={4}>
              <CButton color="info" onClose={() => setVisibleHire(false)} onClick={hideModalHire}>Finalizar Servicio</CButton>
            </CCol>
          </CModalFooter>
          
        </CModal>
        
      )
    }, this);



  let modalServiceFinish = serviceHire.length > 0
    && serviceHire.map((servicio, i) => {
      return (
        <CModal md={9} lg={7} xl={10} visible={visibleFinish} onClose={() => setVisibleFinish(false)}>
          <CModalHeader onClose={() => setVisibleFinish(false)}>
            <CModalTitle>Finalizar Servicio - {servicio.servicio.nombreServicio} </CModalTitle>
          </CModalHeader>
          <CCard className="mx-4">
            <CCardBody className="p-4">
              <CRow className="mb-3">
                <CAlert color="info">Datos De Finalización</CAlert>
                <br></br>
                <CCol md={6}>
                  <CFormLabel htmlFor="IdMunicipio" className="col-sm-10 col-form-label">Motivo Finalización: </CFormLabel>
                  <CFormSelect aria-label="Default select example" value={finalizacionServicio.IdMotivoFinalizacion} onChange={onChangeFinish} name="IdMotivoFinalizacion" id="IdMotivoFinalizacion" label="Tipo de pago">
                    {motivoFinalizacionList}
                  </CFormSelect>
                </CCol>
              </CRow>
              <CForm onSubmit={FinishService}>
                <CFormInput type="hidden" value={finalizacionServicio.IdServicio = servicio.servicio.idServicio} className="form-control form-control-sm" name="IdServicio" id="IdServicio" />
                <CFormInput type="hidden" value={finalizacionServicio.IdVentaServicio = servicio.servicio.idVentaServicio} className="form-control form-control-sm" name="IdVentaServicio" id="IdVentaServicio" />
                <CRow className="mb-3">
                  <CAlert color="info">Calificación Del Servicio</CAlert>
                  <br></br>
                  <CCol md={10}>
                    <CFormLabel htmlFor="Calificacion" className="col-sm-10 col-form-label">Calificación</CFormLabel>
                    <CFormSelect aria-label="Default select example" value={finalizacionServicio.Calificacion} onChange={onChangeFinish} name="Calificacion" id="Calificacion" label="Calificación">
                      <option> Seleccione una opción... </option>
                      <option value="1">★</option>
                      <option value="2">★ ★</option>
                      <option value="3">★ ★ ★</option>
                      <option value="4">★ ★ ★ ★</option>
                      <option value="5">★ ★ ★ ★ ★</option>
                    </CFormSelect>
                  </CCol>
                  <CCol md={10}>
                    <CFormLabel htmlFor="Observacion" className="col-sm-10 col-form-label">Observaciones</CFormLabel>
                    <CFormTextarea rows="3" value={finalizacionServicio.Observacion} onChange={onChangeFinish} name="Observacion" id="Observacion"></CFormTextarea>
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CButton type="submit" color="success">Finalizar Servicio</CButton>
                </CRow>
              </CForm>
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
                <h2>Productos Comprados</h2>
                <br>
                </br>
                <div className="row">
                  {products &&
                    products.map((producto) => (
                      <CCard style={{ width: '22rem' }} key={producto.producto.idProducto}>
                        <CCardImage orientation="top" width="350" height="350" src={producto.producto.imagen} />
                        <CCardBody>
                          <CCardTitle>{producto.producto.nombreProducto}</CCardTitle>
                          <br></br>
                          <div className="d-grid gap-2 d-md-block">
                            {producto.producto.calificacion == 0 ? (<CButton className="btn btn-primary mr-1" onClick={() => showProductQualify(producto.producto.idProducto)}><CIcon icon={cilThumbUp} />Calificar </CButton>) : (
                              <CButton className="btn btn-primary mr-1" onClick={() => showProductQualify(producto.producto.idProducto)} disabled><CIcon icon={cilThumbUp} />Calificar </CButton> )}
                              <CButton className="btn btn-warning" onClick={() => showProductDetails(producto.producto.idProducto)} ><CIcon icon={cilZoom} />Ver Detalles </CButton>
                          </div>
                        </CCardBody>
                      </CCard>
                    ))}
                  {modalProduct}
                  {modalProductDetail}
                </div>
                <h2>Servicios Contratados</h2>
                <br>
                </br>
                <div className="row">
                  {services &&
                    services.map((servicio) => (
                      <CCard style={{ width: '22rem' }} key={servicio.servicio.idServicio}>
                        <CCardImage orientation="top" width="350" height="350" src={servicio.servicio.imagen} />
                        <CCardBody>
                          <CCardTitle>{servicio.servicio.nombreServicio}</CCardTitle>
                          <br></br>
                          <CCardText><strong>Estado De Contratación: </strong> {servicio.servicio.estadoVenta}</CCardText>
                          <CCardText><strong>Motivo De Finalización: </strong> {servicio.servicio.motivoFinalizacion}</CCardText>
                          {servicio.servicio.estadoVenta == "CERRADO" ? (<div className="d-grid gap-2 d-md-block">
                            <CButton color="warning" onClick={() => showServiceHire(servicio.servicio.idServicio)} disabled><CIcon icon={cilSwapHorizontal} /> Gestionar Servicio </CButton>
                          </div>): (
                            <div className = "d-grid gap-2 d-md-block">
                                <CButton color="warning" onClick={() => showServiceHire(servicio.servicio.idServicio)}><CIcon icon = { cilSwapHorizontal } /> Gestionar Servicio </CButton>
                          </div>
                          )}
                        </CCardBody>
                      </CCard>
                    ))}
                  {modalServiceDetail}
                  {modalServiceFinish}
                </div>
              </CCardBody>
            </CCard>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default MyOrders
