import React, { useState, useEffect } from 'react'
import axios from 'axios';
import { Link } from 'react-router-dom'
import {
  CButton,
  CCard,
  CCardBody,
  CCardGroup,
  CCol,
  CContainer,
  CForm,
  CFormInput,
  CInputGroup,
  CAlert,
  CFormSelect,
  CInputGroupText,
  CRow,
  CModal,CModalTitle,CModalBody,CModalFooter,CModalHeader
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilLockLocked, cilUser } from '@coreui/icons'

function Login1(props) {
  const [tipoDocumento, setTipoDocumento] = useState([]);
  const [msgError, setMsgError] = useState('');
  const [visibleRP, setVisibleRP] = useState(false);
  const [visible, setVisible] = useState(false);
  const [visibleCP, setVisibleCP] = useState(false);
  const [visibleErrorCP, setVisibleErrorCP] = useState(false);
  const [userInfo, setUser] = useState({ email: '', password: '' });
  const [userRecoverPass, setUserRecoverPass] = useState({
    email: '',
    tipoDocumento: '',
    documento: ''
  });
  const [infoPass, setInfoPass] = useState({
    Email: '',
    NewPass: ''
  });

  const Login = (e) => {
    e.preventDefault();
    const data = { email: userInfo.email, password: userInfo.password };
    const apiUrl = "https://localhost:44342/users/login";
    axios.post(apiUrl, data)
      .then((result) => {
        console.log("result.data ", result.data);
        const serializedState = JSON.stringify(result.data);
        var a = localStorage.setItem('myData', serializedState);
        console.log("A:", a)
        const user = result.data.token;
        console.log(user);

        if (result.status == 200 && result.data != 'LoginError') {
          if (result.data == "Usuario Inactivo") {
            setMsgError("El usuario se encuentra inactivo, contacte al administrador.");
            setVisible(true);
          } else {
            window.location.href = '#/Dashboard';
          }
        }
        else {
          setMsgError("El usuario y/o contraseña son incorrectos, por favor intente nuevamente");
          setVisible(true);
        }
      })
    
  };

  

  const resetPassword = (email, password) => {
    const apiUrl = "https://localhost:44342/users/resetPassword?UserName=" + email + "&Password=" + password;
    axios
      .post(apiUrl)
      .then(response => {
        if (response.status == 200 && response.data == 'Exitoso') {
          console.log("Contraseña cambiada con exito");
          setVisibleCP(false);
          setMsgError("Contraseña cambiada con exito");
          setVisibleErrorCP(true);
          //setVisibleRP(false);
          //setVisibleCP(true);
        }
        else {
          console.log("Contraseña NO cambiada con exito");
          setMsgError("No se logró cambiar la contraseña exitosamente.")
          setVisibleErrorCP(true);
/*          setVisible(true);*/
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const validateUser = (tipoDocumento,documento,email) => {
    const data = { Email: userRecoverPass.email, Documento: userRecoverPass.documento, TipoDocumento: userRecoverPass.tipoDocumento };
    console.log(data);
    const apiUrl = "https://localhost:44342/users/validateUserRP?TipoDocumento=" + tipoDocumento + "&Documento=" + documento + "&Email=" + email;
    axios
      .get(apiUrl)
      .then(response => {
        if (response.status == 200 && response.data == 'Existe') {
          console.log("Usuario existe");
          setVisibleRP(false);
          setVisibleCP(true);
        }
        else {
          console.log("Usuario no existe");
          setMsgError("Los datos ingresados no concuerdan con ningún usuario registrado");
          setVisibleErrorCP(true);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const GetTipoDocumento = async () => {
    await axios.get("https://localhost:44342/users/getDocumentType")
      .then(response => {
        setTipoDocumento(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
  }

  useEffect(() => {
    GetTipoDocumento();
  }, [])

  let tipoDocumentoList = tipoDocumento.length > 0
    && tipoDocumento.map((item, i) => {
      return (
        <option key={i} value={item.nombre}>{item.nombre}</option>
      )
    }, this);

  const showModalRP = () => {
    setVisibleRP(!visible)
  };

  const onChange = (e) => {
    e.persist();
    setUser({ ...userInfo, [e.target.name]: e.target.value });
  }
  const onChangeRP = (e) => {
    e.persist();
    setUserRecoverPass({ ...userRecoverPass, [e.target.name]: e.target.value });
  }
  const onChangeCP = (e) => {
    e.persist();
    setInfoPass ({ ...infoPass, [e.target.name]: e.target.value });
  }

  return (
    <div className="bg-light min-vh-100 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={8}>
            <CCardGroup>
              <CCard className="p-4">
                <CModal visible={visible} onClose={() => setVisible(false)}>
                  <CModalHeader onClose={() => setVisible(false)}>
                    <CModalTitle>SEI</CModalTitle>
                  </CModalHeader>
                  <CModalBody>{msgError}</CModalBody>
                  <CModalFooter>
                    <CButton color="danger" onClick={() => setVisible(false)}>
                      Aceptar
                    </CButton>
                  </CModalFooter>
                </CModal>
                <CModal visible={visibleErrorCP} onClose={() => setVisibleErrorCP(false)}>
                  <CModalHeader onClose={() => setVisibleErrorCP(false)}>
                    <CModalTitle>SEI</CModalTitle>
                  </CModalHeader>
                  <CModalBody>{msgError}</CModalBody>
                  <CModalFooter>
                    <CButton color="danger" onClick={() => setVisibleErrorCP(false)}>
                      Aceptar
                    </CButton>
                  </CModalFooter>
                </CModal>

                <CModal size="xl" visible={visibleRP} onClose={() => setVisibleRP(false)}>
                  <CModalHeader onClose={() => setVisibleRP(false)}>
                    <CModalTitle>Verificación De Usuario</CModalTitle>
                  </CModalHeader>
                  <CModalBody>
                    <b>Importante:</b><i> Ingrese los datos para validar su usuario y restablecer su contraseña.</i>
                    <CContainer className="px-4">
                      <CRow xs={{ gutterX: 5 }}>
                        <CCol>
                          <div className="p-3 border bg-light">Tipo De Documento
                              <CFormSelect required aria-label="Default select example" value={userRecoverPass.tipoDocumento} onChange={onChangeRP} name="tipoDocumento" id="tipoDocumento">
                                {tipoDocumentoList}
                              </CFormSelect>
                          </div>
                        </CCol>
                        <CCol>
                            <div className="p-3 border bg-light">Número De Documento
                              <CFormInput type="text" className="form-control" value={userRecoverPass.documento} onChange={onChangeRP} name="documento" id="documento" aria-describedby="emailHelp" required />
                          </div>
                        </CCol>
                        <CCol>
                            <div className="p-3 border bg-light">Correo Electronico
                              <CFormInput type="email" className="form-control" value={userRecoverPass.email} onChange={onChangeRP} name="email" id="email" aria-describedby="emailHelp" required />
                          </div>
                        </CCol>
                      </CRow>
                    </CContainer>
                  </CModalBody>
                  <CModalFooter>
                    <CButton color="primary" onClick={() => validateUser(userRecoverPass.tipoDocumento, userRecoverPass.documento, userRecoverPass.email)}>
                      Validar
                    </CButton>
                   </CModalFooter>
                </CModal>

                <CModal size="xl" visible={visibleCP} onClose={() => setVisibleCP(false)}>
                  <CModalHeader onClose={() => setVisibleCP(false)}>
                    <CModalTitle>Restablecer Contraseña</CModalTitle>
                  </CModalHeader>
                  <CModalBody>
                    <b>Importante:</b><i> La contraseña debe tener una longitud de 10 caracteres e incluir una mayúscula y un número.</i>
                    <CContainer className="px-4">
                      <CRow xs={{ gutterX: 5 }}>
                        <CCol>
                          <div className="p-3 border bg-light">Nueva contraseña
                            <CFormInput type="password" className="form-control" onChange={onChangeCP} name="NewPass" id="NewPass" required />
                          </div>
                        </CCol>
                        <CCol>
                          <div className="p-3 border bg-light">Repita la contraseña
                            <CFormInput type="password" className="form-control"  name="rNewPass" id="rNewPass" required />
                          </div>
                        </CCol>
                      </CRow>
                    </CContainer>
                  </CModalBody>
                  <CModalFooter>
                    <CButton color="primary" onClick={() => resetPassword(userRecoverPass.email, infoPass.NewPass)}>
                      Restablecer Contraseña
                    </CButton>
                  </CModalFooter>
                </CModal>

                <CCardBody>
                  <CForm onSubmit={Login} className="user">
                    <h1>SEI</h1>
                    <p className="text-medium-emphasis">Sistema De Empleo Informal</p>
                    <CInputGroup className="mb-3">
                      <CInputGroupText>@</CInputGroupText>
                      <div className="form-group">
                      <CFormInput type="email" className="form-control" value={userInfo.email} onChange={onChange} name="email" id="email" aria-describedby="emailHelp" placeholder="Enter Email"  required/>
                        </div>
                        </CInputGroup>
                    <CInputGroup className="mb-4">
                      <CInputGroupText>
                        <CIcon icon={cilLockLocked} />
                      </CInputGroupText>
                      <div className="form-group">
                      <CFormInput type="password" className="form-control" value={userInfo.password} onChange={onChange} name="password" id="password" placeholder="Password" required/>
                      </div>
                        </CInputGroup>
                    <CRow>
                      <CCol xs={6}>
                        <CButton color="primary" type="submit" className="px-4">
                          Entrar
                        </CButton>
                      </CCol>
                      <CCol xs={6} className="text-right">
                        <CButton color="link" onClick={showModalRP} className="px-0">
                          Restablecer Contraseña
                        </CButton>
                      </CCol>
                    </CRow>
                  </CForm>
                </CCardBody>
              </CCard>
              <CCard className="text-white bg-primary py-5" style={{ width: '44%' }}>
                <CCardBody className="text-center">
                  <div>
                    <h2>Inscribete!</h2>
                    <p>
                      Te ayudamos a encontrar el empleo informal  ideal para ti de forma rápida.
                    </p>
                    <Link to="/register">
                      <CButton color="primary" className="mt-3" active tabIndex={-1}>
                        Registrarse
                      </CButton>
                    </Link>
                  </div>
                </CCardBody>
              </CCard>
            </CCardGroup>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default Login1
