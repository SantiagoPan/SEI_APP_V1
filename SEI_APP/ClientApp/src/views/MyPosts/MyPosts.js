import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  CButton,
  CCard,
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
  CFormSelect,
  CInputGroup,
  CInputGroupText,
  CRow,
  CCardText,
  CFormTextarea,
  cilMagnifyingGlass,


} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilSearch, cilZoom, cilUser, cilEye, cilTrash, cilToggleOff, cilToggleOn } from '@coreui/icons'

function MyPosts(props) {
  const [Posts, setPosts] = useState([]);

  const GetPosts = async () => {
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    axios
      .get("https://localhost:44342/product/getPostsById?idUser=" + infoUser.idUser)
      .then(response => {
        setPosts(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  useEffect(() => {
    GetPosts();
  }, [])

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
                <CTable bordered>
                  <CTableHead>
                    <CTableRow>
                        <CTableHeaderCell scope="col">Publicación</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Precio</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Fecha Publicacíón</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Tipo</CTableHeaderCell>   
                        <CTableHeaderCell scope="col">Estado</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Acciónes</CTableHeaderCell>
                    </CTableRow>
                  </CTableHead>
                  <CTableBody>
                    {Posts &&
                      Posts.map((post) => (
                        <CTableRow>
                          <CTableDataCell>{post.nombre}</CTableDataCell>
                          <CTableDataCell>{post.precio}</CTableDataCell>
                          <CTableDataCell>{post.fechaPublicacion}</CTableDataCell>
                          <CTableDataCell>{post.tipo}</CTableDataCell>
                          <CTableDataCell>{post.estado}</CTableDataCell>
                          {post.estado == "ACTIVO" ? (
                            <CTableDataCell>
                              <CTooltip content="Ver Detalles" placement="bottom"><CButton color="light" className="rounded-pill" type="button"><CIcon icon={cilZoom} /></CButton></CTooltip>
                              | <CTooltip content="Desactivar" placement="bottom"><CButton color="light" className="rounded-pill" type="button"><CIcon icon={cilToggleOn} /></CButton></CTooltip>
                              | <CTooltip content="Eliminar" placement="bottom"><CButton color="light" className="rounded-pill" type="button"><CIcon icon={cilTrash}/></CButton></CTooltip>
                             </CTableDataCell>
                          ) : (
                              <CTableDataCell>
                                <CTooltip content="Ver Detalles" placement="bottom"><CButton color="light" className="rounded-pill" type="button"><CIcon icon={cilZoom} /></CButton></CTooltip>
                                | <CTooltip content="Activar" placement="bottom"><CButton color="light" className="rounded-pill" type="button"><CIcon icon={cilToggleOff} /></CButton></CTooltip>
                                | <CTooltip content="Eliminar" placement="bottom"><CButton color="light" className="rounded-pill" type="button"><CIcon icon={cilTrash} className="x-2" /></CButton></CTooltip>
                             </CTableDataCell>
                          )}
                        </CTableRow>
                      ))}
                  </CTableBody>
                </CTable>
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
