import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  CButton,
  CCard,
  CCardBody,
  CCol,
  CContainer,
  CForm,
  CRow,
  CInputGroup,
  CInputGroupText,
  CFormInput,
  CCardImage,
  CCardTitle,
  CCardText,
  CModal,
  CModalHeader,
  CFormSelect,
  CFormLabel,
  CModalFooter,
  CModalTitle,
  CAlert,
  CCardSubtitle,

} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilCart, cilSearch, cilZoom } from '@coreui/icons'


function HireService1(props) {
  const [services, setDataServices] = useState([]);
  const [servicesFiltered, setServicesFiltered] = useState([]);
  const [inputText, setInputText] = useState("");
  const [tipoPago, setTipoPago] = useState([]);
  const [serviceDetModal, setServiceDetModal] = useState([]);
  const [serviceBuyModal, setServiceBuyModal] = useState([]);
  const [visibleHire, setVisibleHire] = useState(false)
  const [dataContrato, setDataContrato] = useState({
    IdUser: '',
    IdServicio: 0,
    IdTipoPago: 0
  });

  const GetServices = async () => {
    await axios.get("https://localhost:44342/services/getServices")
      .then(response => {
        setDataServices(response.data)

      })
      .catch((error) => {
        console.log(error);
      });
  }
  const GetTipoPago = async () => {
    await axios.get("https://localhost:44342/product/getTypePayment")
      .then(response => {
        setTipoPago(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
  }

  useEffect(() => {
    GetServices();
    GetTipoPago();
  }, [])

  let tipoPagoList = tipoPago.length > 0
    && tipoPago.map((item, i) => {
      return (
        <option key={i} value={item.idTipoPago}>{item.nombre}</option>
      )
    }, this);

  const onChange = (e) => {
    e.persist();
    setDataContrato({ ...dataContrato, [e.target.name]: e.target.value });
  }


  const HireService = (e) => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    dataContrato.IdUser = infoUser.idUser
    e.preventDefault();

    const data = {
      IdUsuario: dataContrato.IdUser,
      IdTipoPago: dataContrato.IdTipoPago,
      IdServicio : dataContrato.IdServicio
    };
    const apiUrl = "https://localhost:44342/services/hireService";
    axios.post(apiUrl, data)
      .then((result) => {
        console.log(result.data);
        const serializedState = JSON.stringify(result.data);
        var a = localStorage.setItem('HiredServices', serializedState);
        console.log("A:", a)
        const user = result.data.token;
        console.log(user);
        if (result.status == 200)
          window.location.reload(true);
        else
          alert('No registrado');
      })
  };

  const handleChange = e => {
    setBusqueda(e.target.value);
    filtrar(e.target.value);
  }

  let inputHandler = (e) => {
    var lowerCase = e.target.value.toLowerCase();
    setInputText(lowerCase);
  };

  const showModalHire = (idServicio) => {
    let serviceBuyModal = services.filter(v => v.servicio.idServicio == idServicio);
    setServiceBuyModal(serviceBuyModal)
    setVisibleHire(!visibleHire)
  };

  let modalBuyService = serviceBuyModal.length > 0
    && serviceBuyModal.map((item, i) => {
      return (
        <CModal visible={visibleHire} onClose={() => setVisibleHire(false)}>
          <CModalHeader onClose={() => setVisibleHire(false)}>
            <CModalTitle>Datos del servicio - {item.servicio.nombreServicio}</CModalTitle>
          </CModalHeader>
          <CCard className="mx-4">
            <CCardBody className="p-4">
              <CForm onSubmit={HireService} className="user">
                <CFormInput type="hidden" value={dataContrato.IdServicio = item.servicio.idServicio} className="form-control form-control-sm" name="IdServicio" id="IdServicio" />
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
                <CCol md={6}>
                  <CButton type="submit" color="success">Contratar</CButton>
                </CCol>
              </CForm>
            </CCardBody>
          </CCard>
          <CModalFooter>
          </CModalFooter>
        </CModal>
      )
    }, this);

  const searchService = () => {
    let servicesFilter = services.filter(v => v.servicio.nombreServicio.toLowerCase().includes(inputText));
    if (inputText != "") {
      document.getElementsByClassName('allServices')[0].style.visibility = 'hidden';
      setServicesFiltered(servicesFilter);
    } else {
      document.getElementsByClassName('allServices')[0].style.visibility = 'visible';
      console.log("Sin resultados en la busqueda");
    }
  };

  let servicesFilter = servicesFiltered.length > 0
    && servicesFiltered.map((item, i) => {
      return (
          <div className="row">
          {servicesFiltered &&
            servicesFiltered.map((servicio) => (
                <CCard style={{ width: '22rem' }} key={servicio.servicio.idServicio}>
                <CCardImage orientation="top" width="350" height="350" src={servicio.servicio.imagen} />
                  <CCardBody>
                    <CCardTitle>{servicio.servicio.nombreServicio} | <small>{servicio.servicio.localizacion.ciudad}, {servicio.servicio.localizacion.departmento}</small></CCardTitle>
                    <br></br>
                    <CCardSubtitle><strong>Descripción</strong> </CCardSubtitle>
                    <CCardText>{servicio.servicio.descripcion}</CCardText>
                    {servicio.servicio.aplicaConvenio == "Si" ? (
                      <CCardText><strong>Tipo de pago: </strong> A convenir con el prestador</CCardText>
                    ) : (
                      <CCardText><strong>Precio: </strong>{servicio.servicio.costoServicio}</CCardText>
                    )}
                    <div className="d-grid gap-2 d-md-block">
                      <CButton href="" color="primary" onClick={showModalHire} key={servicio.servicio.idServicio}><CIcon icon={cilCart} />Contratar </CButton>
                    </div>
                  </CCardBody>
                </CCard>
              ))}

          {servicesFiltered &&
            servicesFiltered.map((servicio) => (
                <CModal visible={visibleHire} onClose={() => setVisibleHire(false)}>
                  <CModalHeader onClose={() => setVisibleHire(false)}>
                    <CModalTitle>Datos del servicio - {servicio.servicio.nombreServicio}</CModalTitle>
                  </CModalHeader>
                  <CCard className="mx-4">
                    <CCardBody className="p-4">
                      <CForm onSubmit={HireService} className="user">
                        <CFormInput type="hidden" value={dataContrato.IdServicio = servicio.servicio.idServicio} className="form-control form-control-sm" name="IdServicio" id="IdServicio" />
                        <CRow className="mb-3">
                          <CCardSubtitle><strong>Descripción</strong> </CCardSubtitle>
                          <CCardText>{servicio.servicio.descripcion}</CCardText>
                          <CCardText><strong>Calificación: </strong>{servicio.servicio.calificacion}</CCardText>
                          <CCardText><strong>Categoría: </strong>{servicio.servicio.tipoServicio}</CCardText>
                          <CCardText><strong>Experiencia: </strong>{servicio.servicio.caracteristicas.experiencia}</CCardText>
                          <CCardText><strong>Incluye: </strong>{servicio.servicio.caracteristicas.incluye}</CCardText>
                          <CCardText><strong>No Incluye: </strong>{servicio.servicio.caracteristicas.noIncluye}</CCardText>
                          <CAlert color="info">Datos Del Prestador</CAlert>
                          <CCardText><strong>Nombre: </strong>{servicio.servicio.caracteristicas.nombre}</CCardText>
                          <CCardText><strong>Teléfono: </strong>{servicio.servicio.caracteristicas.telefono}</CCardText>
                          <CCardText><strong>Ubicación: </strong>{servicio.servicio.localizacion.ciudad}, {servicio.servicio.localizacion.departmento} <b> Barrio: </b> {servicio.servicio.localizacion.barrio}</CCardText>
                          {servicio.servicio.aplicaConvenio == "Si" ? (
                            <CCardText><strong>Tipo de pago: </strong> A convenir con el prestador</CCardText>
                          ) : (
                            <CCardText><strong>Precio: </strong>{servicio.servicio.costoServicio}</CCardText>
                          )}
                        </CRow>
                        <CCol md={6}>
                          <CButton type="submit" color="success">Contratar</CButton>
                        </CCol>
                      </CForm>
                    </CCardBody>
                  </CCard>
                  <CModalFooter>
                  </CModalFooter>
                </CModal>
              ))}
          </div>
      )
    }, this);

  return (
    <div className="bg-light min-vh-10 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={9} lg={7} xl={12}>
            <CCard className="mx-4">
              <CCardBody className="p-4">
                <h2>Servicios Disponibles</h2>
                  <div>
                    <CInputGroup className="input-prepend">
                      <CInputGroupText>
                        <CIcon icon={cilSearch} />
                      </CInputGroupText>
                      <CFormInput type="text" onChange={inputHandler} required id="strSearch" name="strSearch" />
                      <CButton color="primary" onClick={searchService} >Buscar Servicio</CButton>
                    </CInputGroup>
                  </div>
                <br>
                </br>
                <div className="servicesFilter">
                  {servicesFilter}
                </div>
                <div className="allServices">
                <div className="row">
                  {services &&
                    services.map((servicio) => (
                      <CCard style={{ width: '22rem' }} key={servicio.servicio.idServicio}>
                        <CCardImage orientation="top" width="350" height="350" src={servicio.servicio.imagen} />
                        <CCardBody>
                          <CCardTitle>{servicio.servicio.nombreServicio} | <small>{servicio.servicio.localizacion.ciudad}, {servicio.servicio.localizacion.departmento}</small></CCardTitle>
                          <br></br>
                          <CCardSubtitle><strong>Descripción</strong> </CCardSubtitle>
                          <CCardText>{servicio.servicio.descripcion}</CCardText>
                          {servicio.servicio.aplicaConvenio == "Si" ? (
                            <CCardText><strong>Tipo de pago: </strong> A convenir con el prestador</CCardText>
                          ) : (
                            <CCardText><strong>Precio: </strong>{servicio.servicio.costoServicio}</CCardText>
                          )}
                          <div className="d-grid gap-2 d-md-block">
                            <CButton href="" color="primary" onClick={() => showModalHire(servicio.servicio.idServicio)} key={servicio.servicio.idServicio}><CIcon icon={cilCart} />Contratar </CButton>
                          </div>
                        </CCardBody>
                      </CCard>
                    ))}
                    <div>
                      {modalBuyService}
                    </div>
                  </div>
                </div>
              </CCardBody>
            </CCard>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default HireService1
