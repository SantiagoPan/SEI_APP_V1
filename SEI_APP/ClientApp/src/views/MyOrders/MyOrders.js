import axios from 'axios';
import React, { useState, useEffect } from 'react';
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
  CCardSubtitle,
  CModal,
  CFormCheck,
  CFormLabel,
  CModalBody,
  CModalHeader,
  CModalFooter,
  CModalTitle, CFormSelect,
  CAlert
} from '@coreui/react'
import { cilCart, cilSearch, cilZoom } from '@coreui/icons'
import { DocsCallout, DocsExample } from 'src/components'



const MyOrders = () => {

  const [products, setDataProducts] = useState([]);
  const [services, setDataServices] = useState([]);

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


  useEffect(() => {
    GetProductos();
    GetServicios();
  }, [])

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
                      <CCard style={{ width: '18rem' }} key={producto.producto.idProducto}>
                        <CCardImage orientation="top" src={producto.producto.imagen} />
                        <CCardBody>
                          <CCardTitle>{producto.producto.nombreProducto}</CCardTitle>
                          <br></br>
                          <CCardText><strong>Estado De Venta: </strong> {producto.producto.estadoVenta}</CCardText>
                          <div className="d-grid gap-2 d-md-block">
                           {/* <CButton color="primary"><CIcon icon={cilCart} />Ver Compra </CButton>*/}
                          </div>

                        </CCardBody>
                      </CCard>
                    ))}
                </div>
                <h2>Servicios Contratados</h2>
                <br>
                </br>
                <div className="row">
                  {services &&
                    services.map((servicio) => (
                      <CCard style={{ width: '18rem' }} key={servicio.servicio.idServicio}>
                        <CCardImage orientation="top" src={servicio.servicio.imagen} />
                        <CCardBody>
                          <CCardTitle>{servicio.servicio.nombreServicio}</CCardTitle>
                          <br></br>
                          <CCardText><strong>Estado De Contratación: </strong> {servicio.servicio.estadoVenta}</CCardText>
                          <CCardText><strong>Motivo De Finalización: </strong> {servicio.servicio.motivoFinalizacion}</CCardText>
                          <div className="d-grid gap-2 d-md-block">
                            {/* <CButton color="primary"><CIcon icon={cilCart} />Ver Compra </CButton>*/}
                          </div>
                        </CCardBody>
                      </CCard>
                    ))}
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
