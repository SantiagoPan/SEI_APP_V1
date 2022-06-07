import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  CButton,
  CCard,
  CCardBody,
  cilCart,
  CCol,
  CContainer,
  CCardTitle,
  CTable,
  CTableBody,
  CTableHead,
  CTableRow,
  CTableDataCell,
  CTableHeaderCell,
  CForm,
  CModal,
  CModalHeader,
  CModalFooter,
  CModalTitle,
  CFormLabel,
  CFormInput,
  CFormSelect,
  CInputGroup,
  CInputGroupText,
  CRow,
  CCardText,
  CFormTextarea,
  cilMagnifyingGlass,


} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilSearch, cilLockLocked, cilUser, cilPhone, cilAddressBook, cilBirthdayCake } from '@coreui/icons'

function Message(props) {
  const [messagesSales, setMessagesSales] = useState([]);
  const [visibleQualify, setVisibleQualify] = useState(false);
  const [messagesBuys, setMessagesBuys] = useState([]);
  const [notifications, setNotifications] = useState([]);
  const [respuestaVenta, setRespuestaVenta] = useState({
    IdUsuario: '',
    IdMensaje: 0,
    IdMensajesVentaServicio: 0,
    IdVentaServicio: 0,
    Respuesta: ''
  });
  
  const GetMessagesSales = async () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    axios
      .get("https://localhost:44342/services/getMessagesSalesById?idUser=" + infoUser.idUser)
      .then(response => {
        setMessagesSales(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  const GetMessagesBuys = async () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    axios
      .get("https://localhost:44342/services/getMessagesBuysById?idUser=" + infoUser.idUser)
      .then(response => {
        setMessagesBuys(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  const GetNotifications = async () => {
    axios
      .get("https://localhost:44342/users/getNotificationAdmin")
      .then(response => {
        setNotifications(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  const showModalQualify = (IdMensajesVentaServicio) => {
    console.log(IdMensajesVentaServicio);
    respuestaVenta.IdMensajesVentaServicio = IdMensajesVentaServicio;
    setVisibleQualify(!visibleQualify)
  };

  const SendResponseSale = () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    const data = {
      IdUsuario: infoUser.idUser,
      IdMensajesVentaServicio: respuestaVenta.IdMensajesVentaServicio,
      Mensaje: respuestaVenta.Respuesta,
    };
    const apiUrl = "https://localhost:44342/services/sendResponseMessageS";
    axios.post(apiUrl, data)
      .then((result) => {
        console.log(result.data);
        if (result.status == 200)
          window.location.reload(true);
        else
          alert('No registrado');
      })
  };


  const onChangeVenta = (e) => {
    e.persist();
    setRespuestaVenta({ ...respuestaVenta, [e.target.name]: e.target.value });
  }

  useEffect(() => {
    GetMessagesSales();
    GetNotifications();
    GetMessagesBuys();
  }, [])

  return (
        <div className="bg-light min-vh-10 d-flex flex-row align-items-center">
      <CContainer>
        <div>
        <CRow className="justify-content-center">
          <CCol md={9} lg={7} xl={12}>
            <CCard className="mx-4">
              <CCardBody className="p-4">
                <h2>Mensajes De Mis Ventas</h2>
              </CCardBody>
              <div className="justify-content-center">
                {messagesSales.length == 0 ? (<h4>No tiene Mensajes</h4>) : (
                  <CTable bordered>
                    <CTableHead>
                      <CTableRow>
                        <CTableHeaderCell scope="col">#</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Servicio</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Mensaje</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Respuesta</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Fecha Del Mensaje</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Nombre Del Coprador</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Acciones</CTableHeaderCell>
                      </CTableRow>
                    </CTableHead>
                    <CTableBody>
                      {messagesSales &&
                        messagesSales.map((mensaje) => (
                          <CTableRow>
                            <CTableHeaderCell scope="row">{mensaje.idMensaje}</CTableHeaderCell>
                            <CTableDataCell>{mensaje.servicio}</CTableDataCell>
                            <CTableDataCell>{mensaje.mensaje}</CTableDataCell>
                             <CTableDataCell>{mensaje.respuesta}</CTableDataCell>
                            <CTableDataCell>{mensaje.fecha}</CTableDataCell>
                            <CTableDataCell>{mensaje.nombreVendedor}</CTableDataCell>
                            {mensaje.respuesta == null ? (
                              <CTableDataCell><CButton color="success" onClick={() => showModalQualify(mensaje.idMensaje)}> Responder</CButton></CTableDataCell>
                            ) : (
                                <CTableDataCell><CButton color="success" disabled onClick={() => showModalQualify(mensaje.idMensaje)}> Responder</CButton></CTableDataCell>
                            )}
                            
                          </CTableRow>
                        ))}
                    </CTableBody>
                  </CTable>
                )}
              </div>

              <CModal visible={visibleQualify} onClose={() => setVisibleQualify(false)}>
                <CModalHeader onClose={() => setVisibleQualify(false)}>
                  <CModalTitle>Responder</CModalTitle>
                </CModalHeader>
                <CCard className="mx-6">
                  <CCardBody className="p-6">
                    <CForm onSubmit={SendResponseSale} className="row g-3">
                      <CCol md={10}>
                        <CFormLabel htmlFor="Observacion" className="col-sm-10 col-form-label">Mensaje</CFormLabel>
                        <CFormTextarea rows="3" value={respuestaVenta.Respuesta} onChange={onChangeVenta} name="Respuesta" id="Respuesta"></CFormTextarea>
                      </CCol>
                      <CCol md={6}>
                        <CButton type="submit" color="success">Enviar Respuesta</CButton>
                      </CCol>
                    </CForm>
                  </CCardBody>
                </CCard>
                <CModalFooter>
                </CModalFooter>
              </CModal>
              <CCardBody className="p-4">
                <h2>Mensajes De Mis Compras</h2>
              </CCardBody>
              <div className="justify-content-center">
                {messagesBuys.length == 0 ? (<h4>No tiene Mensajes</h4>): (
                  <CTable bordered>
                    <CTableHead>
                      <CTableRow>
                        <CTableHeaderCell scope="col">#</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Servicio</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Mensaje</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Respuesta Vendedor</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Fecha Del Mensaje</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Nombre Del Vendedor</CTableHeaderCell>
                      </CTableRow>
                    </CTableHead>
                    <CTableBody>
                      {messagesBuys &&
                        messagesBuys.map((mensaje) => (
                          <CTableRow>
                            <CTableHeaderCell scope="row">{mensaje.idMensaje}</CTableHeaderCell>
                            <CTableDataCell>{mensaje.servicio}</CTableDataCell>
                            <CTableDataCell>{mensaje.mensaje}</CTableDataCell>
                            <CTableDataCell>{mensaje.respuesta}</CTableDataCell>
                            <CTableDataCell>{mensaje.fecha}</CTableDataCell>
                            <CTableDataCell>{mensaje.nombreVendedor}</CTableDataCell>
                          </CTableRow>
                        ))}
                    </CTableBody>
                  </CTable>
                )}

                <CCardBody className="p-4">
                  <h2>Notificaciones Del Administrador</h2>
                </CCardBody>
                <div className="justify-content-center">
                  {notifications.length == 0 ? (<h4>No tiense notificaciones</h4>) : (
                    <CTable bordered>
                      <CTableHead>
                        <CTableRow>
                          <CTableHeaderCell scope="col">#</CTableHeaderCell>
                          <CTableHeaderCell scope="col">Mensaje</CTableHeaderCell>
                          <CTableHeaderCell scope="col">Fecha Del Mensaje</CTableHeaderCell>
                        </CTableRow>
                      </CTableHead>
                      <CTableBody>
                        {notifications &&
                          notifications.map((mensaje) => (
                            <CTableRow>
                              <CTableHeaderCell scope="row">{mensaje.idNotificacionMasiva}</CTableHeaderCell>
                              <CTableDataCell>{mensaje.mensaje}</CTableDataCell>
                              <CTableDataCell>{mensaje.fechaMensaje}</CTableDataCell>

                            </CTableRow>
                          ))}
                      </CTableBody>
                    </CTable>

                  )}
                </div>
              </div>
            </CCard>
          </CCol>
        </CRow>
        </div>
      </CContainer>
    </div>
  )
}

export default Message
