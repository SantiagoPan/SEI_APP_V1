import React, { useState, useEffect} from 'react';
import axios from 'axios';
import {
  CButton,
  CCard,
  CCardBody,
  CCol,
  CContainer,
  CForm,
  CTable,
  CTableBody,
  CTableHead,
  CTableRow,
  CTableDataCell,
  CTableHeaderCell,
  CFormInput,
  CFormSelect,
  CInputGroup,
  CInputGroupText,
  CRow,
  CFormTextarea,
  cilMagnifyingGlass,
  CCardImage,
  CTooltip,
  CCardTitle,
  CCardText,
  CModal,
  CModalBody,
  CModalHeader,
  CModalFooter,
  CModalTitle,
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilToggleOff, cilToggleOn } from '@coreui/icons'

function MassiveNotification(props) {
  const [msg, setMsg] = useState({
    Mensaje : ''
  });
  const [notifications, setNotifications] = useState([]);

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
  const SendNotification = () => {
    const data = {
      Mensaje: msg.Mensaje
    };
    console.log(data);
    const apiUrl = "https://localhost:44342/users/sendNotification";
    axios.post(apiUrl, data)
      .then((result) => {
        console.log(result.data);
        if (result.status == 200)
          window.location.reload(true);
        else
          alert('Notificación No Enviada');
      })
  };

  const onChange = (e) => {
    e.persist();
    setMsg({ ...msg, [e.target.name]: e.target.value });
  }
  useEffect(() => {
    GetNotifications();
  }, [])
  return (
    <div className="bg-light min-vh-10 d-flex flex-row align-items-center">
      <CContainer className="px-12">
        <CRow xs={{ gutterX: 6 }}>
          <CCol>
            <CCard style={{ width: '50rem' }}>
              <h4>Enviar Notificación A Los Usuarios</h4>
              <CCardBody>
                <CForm onSubmit={SendNotification} className="row g-3">
                  <CFormTextarea value={msg.Mensaje} onChange={onChange} rows="3" name="Mensaje" id="Mensaje"></CFormTextarea>
                  <CButton color="primary" type="submit">Notificar</CButton>
                  </CForm>
              </CCardBody>

              <CCardBody className="p-4">
                <h4>Notificaciones Del Administrador</h4>
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
            </CCard>
          </CCol>
        </CRow>
      </CContainer>
    </div>


  )
}

export default MassiveNotification
