import React, { useState } from 'react';
import axios from 'axios';
import {
  CButton,
  CCard,
  CCardBody,
  CCol,
  CContainer,
  CForm,
  CFormInput,
  CFormSelect,
  CInputGroup,
  CInputGroupText,
  CRow,
} from '@coreui/react'

import CIcon from '@coreui/icons-react'
import { cilUser, cilAddressBook, cilBank, cilCreditCard, cilDialpad } from '@coreui/icons'


function RegisterBank1(props) {
  const [tipoDocumentoIdentidad, setTipoDocumentoIdentidad] = useState([]);
  const [userInfo, setUser] = useState({

    firstname: '',
    document: '',
    address: '',
    bankName: 0,
    accountType: 0,
    accountNumber: 0,
  });

    const apiUrl = "https://localhost:44342/users/register";
    const SaveBank = (e) => {
      e.preventDefault();
      const data = {
        tipoDocumentoIdentidad: tipoDocumentoIdentidad

      };
      axios.post(apiUrl, data)
        .then((result) => {
          debugger;
          console.log(result.data);
          const serializedState = JSON.stringify(result.data.UserDetails);
          var a = localStorage.setItem('myData', serializedState);
          console.log("A:", a)
          const user = result.data.token;
          console.log(user);
          if (result.status == 200)
            window.location.href = '/Login';
          else
            alert('No registrado');
        })
    };
    let tipoDocumentoIdentidadList = tipoDocumentoIdentidad.length > 0
      && tipoDocumentoIdentidad.map((item, i) => {
        return (
          <option key={i} value={item.idtipoDocumentoIdentidad}>{item.nombre}</option>
        )
      }, this);

    const onChangeTipoDocumentoIdentidad = (e) => {
      let idtipoDocumentoIdentidad = e.target.value;
      datatipoDocumentoIdentidad.idtipoDocumentoIdentidad = e.target.value
      axios
        .get("https://localhost:44342/services/getCities?idtipoDocumentoIdentidad=" + idtipoDocumentoIdentidad)
        .then(response => {
          setTipoDocumentoIdentidad(response.data)
          setTimeout(() => {
            console.log(municipios);
          }, 3000)
        })
        .catch((error) => {
          console.log(error);
        });
    }
    const onChange = (e) => {
      e.persist();
      setUser({ ...userInfo, [e.target.name]: e.target.value });
    }
    return (
      <div className="bg-light min-vh-50 d-flex flex-row align-items-center">

        <CContainer >
          <CRow className="justify-content-center">
            <CCol md={9} lg={7} xl={7}>
              <CCard className="mx-20">
                <CCardBody className="p-4">
                  <CForm onSubmit={SaveBank} className="user">

                    <h3>  <center> REGISTRO DE CUENTA BANCARIA </center> </h3>
                    <fieldset> <p className="text-medium-emphasis">DATOS DEL TITULAR DE LA CUENTA</p>

                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilUser} />
                        </CInputGroupText>
                        <CFormInput placeholder="Nombre Completo" required value={userInfo.firstname} onChange={onChange} autoComplete="firstname" name="firstname" id="firstname" />
                      </CInputGroup>

                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilUser} />
                        </CInputGroupText>
                        <CFormSelect aria-label="Default select example" required onChange={onChange} value={onChangeTipoDocumentoIdentidad} name="documenttype" id="documenttype">
                          {tipoDocumentoIdentidadList}
                        </CFormSelect>
                      </CInputGroup>

                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilUser} />
                        </CInputGroupText>
                        <CFormInput placeholder="Número De Documento" required value={userInfo.document} onChange={onChange} autoComplete="document" name="document" id="document" />
                      </CInputGroup>


                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilAddressBook} />
                        </CInputGroupText>
                        <CFormInput placeholder="Dirección" required value={userInfo.address} onChange={onChange} autoComplete="address" name="address" id="address" />
                      </CInputGroup>


                      <p className="text-medium-emphasis">DATOS DE LA CUENTA</p>
                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilBank} />
                        </CInputGroupText>
                        <CFormSelect aria-label="Default select example" required onChange={onChange} value={userInfo.bankName} name="bankName" id="bankName">
                          <option>Entidad Bancaria</option>
                          <option value="Bancolombia">Bancolombia</option>
                          <option value="BBVA">BBVA</option>
                          <option value="Davivienda">Davivienda</option>
                        </CFormSelect>
                      </CInputGroup>

                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilCreditCard} />
                        </CInputGroupText>
                        <CFormSelect aria-label="Default select example" required onChange={onChange} value={userInfo.accountType} name="accountType" id="accountType">
                          <option>Tipo De Cuenta</option>
                          <option value="Ahorros">Ahorros</option>
                          <option value="Corriente">Corriente</option>
                        </CFormSelect>
                      </CInputGroup>

                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilDialpad} />
                        </CInputGroupText>
                        <CFormInput placeholder="Numero De Cuenta" required value={userInfo.accountNumber} onChange={onChange} autoComplete="accountNumber" name="accountNumber" id="accountNumber" />
                      </CInputGroup>
                    </fieldset>

                    <div className="d-grid">
                      <CButton color="success" type="submit"> Guardar</CButton>

                      <CButton color="primary" className="mt-3"> Cancelar</CButton>
                    </div>
                  </CForm>
                </CCardBody>
              </CCard>
            </CCol>
          </CRow>
        </CContainer>
      </div>
    )
  }
  
export default RegisterBank1
