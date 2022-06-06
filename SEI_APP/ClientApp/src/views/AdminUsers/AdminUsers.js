import React, { useState, useEffect, images, FileUploader } from 'react';
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
import { cilUser, cilPhone, cilAddressBook, cilBirthdayCake, cilSearch, cilLockLocked, cilCloudDownload, cilTrash, cilPencil, cilToggleOff, cilToggleOn } from '@coreui/icons'

function AdminUsers(props) {
  const [users, setUsers] = useState([]);
  const [tipoDocumento, setTipoDocumento] = useState([]);
  const [visible, setVisible] = useState(false);
  const [visibleEdit, setVisibleEdit] = useState(false);
  const [userInfoToUpdate, setUserInfoToUpdate] = useState([]);
  const [userInfo, setUserInfo] = useState([]);
  const [dataUser, setDataUser] = useState({
    idUser: '',
    address: '',
    birthdate: '',
    document: '',
    documentType: '',
    email: '',
    firstName: '',
    idUser: '',
    lastname: '',
    phone: '',
  });
  const changeStateUser = (idUsuario) => {
    axios
      .get("https://localhost:44342/users/updateStateUser?idUsuario=" + idUsuario)
      .then(response => {
        window.location.reload(true);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const updateData = (e) => {
    const apiUrl = "https://localhost:44342/users/updateInfoUser";
    e.preventDefault();
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    const data = {
      idUser: infoUser.idUser,
      firstname: userInfo[0].user.firstName,
      lastname: userInfo[0].user.lastname,
      username: userInfo[0].user.username,
      documentType: userInfo[0].user.documentType,
      document: userInfo[0].user.document,
      birthdate: userInfo[0].user.birthdate,
      phone: userInfo[0].user.phone,
      address: userInfo[0].user.address,
      email: userInfo[0].user.email,
      password: userInfo[0].user.password,
    };
    console.log(data);
    axios.post(apiUrl, data)
      .then((result) => {
        console.log(result.data);
        const serializedState = JSON.stringify(result.data.UserDetails);
        var a = localStorage.setItem('MyData', serializedState);
        console.log("A:", a)
        const user = result.data.token;
        console.log(user);
        if (result.status == 200)
          window.location.reload(true);
        else
          alert('No registrado');
      })
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

  const GetUsers = async () => {
    axios
      .get("https://localhost:44342/users/getUsers")
      .then(response => {
        setUsers(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
  }

  useEffect(() => {
    GetUsers();
    GetTipoDocumento();
  }, [])

  const showModalUsers = () => {
    setVisible(!visible)
  };

  let tipoDocumentoList = tipoDocumento.length > 0
    && tipoDocumento.map((item, i) => {
      return (
        <option key={i} value={item.nombre}>{item.nombre}</option>
      )
    }, this);

  const onChange = (e) => {
    e.persist();
    setDataUser({ ...dataUser, [e.target.name]: e.target.value });
    userInfo[0].user[e.target.name]= e.target.value;
    /*console.log(dataUser);*/
    console.log(userInfo[0].user.firstName);
    
  }
  const editUser = (idUser) => {
    setVisible(false);
    let userInfo = users.filter(v => v.user.idUser == idUser);
    setUserInfo(userInfo);
    setDataUser(userInfo);
    setVisibleEdit(!visibleEdit);
  };

  let modalUpdateUser = userInfo.length > 0
    && userInfo.map((item, i) => {
      return (
        <CModal
          visible={visibleEdit} onClose={() => setVisibleEdit(false)}>
          <CModalHeader onClose={() => setVisibleEdit(false)}>
            <CModalTitle>Editar Usuario</CModalTitle>
          </CModalHeader>
          <CCard className="mx-4">
            <CCardBody className="p-4">
              <CForm onSubmit={updateData} className="user">
                <h2>Mis Datos</h2>
                <CInputGroup className="mb-3">
                  <CInputGroupText>
                    <CIcon icon={cilUser} />
                  </CInputGroupText>
                  <CFormInput placeholder="Nombres" defaultValue={item.user.firstName} value={dataUser.firstName} onChange={ onChange }  autoComplete="firstName" name="firstName" id="firstname" />
                </CInputGroup>
                <CInputGroup className="mb-3">
                  <CInputGroupText>
                    <CIcon icon={cilUser} />
                  </CInputGroupText>
                  <CFormInput placeholder="Apellidos" defaultValue={item.user.lastname} onChange={onChange} autoComplete="lastname" name="lastname" id="lastname" />
                </CInputGroup>

                <CInputGroup className="mb-3">
                  <CInputGroupText>
                    <CIcon icon={cilUser} />
                  </CInputGroupText>
                  <CFormSelect aria-label="Default select example" defaultValue={item.user.documentType} onChange={onChange} name="documentType" id="documentType">
                    {tipoDocumentoList}
                  </CFormSelect>
                </CInputGroup>

                <CInputGroup className="mb-3">
                  <CInputGroupText>
                    <CIcon icon={cilUser} />
                  </CInputGroupText>
                  <CFormInput placeholder="Número De Documento" defaultValue={item.user.document} onChange={onChange} autoComplete="document" name="document" id="document" />
                </CInputGroup>

                <CInputGroup className="mb-3">
                  <CInputGroupText>
                    <CIcon icon={cilBirthdayCake} />
                  </CInputGroupText>
                  <CFormInput type="date" placeholder="Fecha de nacimiento" defaultValue={item.user.birthdate} onChange={onChange} name="birthdate" id="birthdate" />
                </CInputGroup>

                <CInputGroup className="mb-3">
                  <CInputGroupText>
                    <CIcon icon={cilPhone} />
                  </CInputGroupText>
                  <CFormInput placeholder="Telefono" defaultValue={item.user.phone} onChange={onChange} autoComplete="phone" name="phone" id="phone" />
                </CInputGroup>

                <CInputGroup className="mb-3">
                  <CInputGroupText>
                    <CIcon icon={cilAddressBook} />
                  </CInputGroupText>
                  <CFormInput placeholder="Dirección" defaultValue={item.user.address} onChange={onChange} autoComplete="address" name="address" id="address" />
                </CInputGroup>
                <div className="d-grid">
                  <CButton color="success" type="submit" >Actualizar Datos</CButton>
                </div>
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
      <CContainer className="px-12">
        <CRow xs={{ gutterX: 3 }}>
          <CCol>
            <CCard style={{ width: '30rem' }}>
              <CCardBody>
                <CCardImage orientation="top" src="https://blog.ida.cl/wp-content/uploads/sites/5/2022/05/LinkedIn-mejora-sus-pol%C3%ADticas-de-seguridad-Blog-470x272.png" />
                <CCardText>
                  Gestiona la información de los usuarios registrados.
                </CCardText>
                <CButton color="dark" onClick={showModalUsers}>Administrar Usuarios</CButton>
              </CCardBody>
            </CCard>
          </CCol>
        </CRow>
        <CModal size="lg" visible={visible} onClose={() => setVisible(false)}>
          <CModalHeader onClose={() => setVisible(false)}>
            <CModalTitle>USUARIOS</CModalTitle>
          </CModalHeader>
          <CCard className="mx-4" >
            <CCardBody className="p-4">
              <CForm className="user">
                <CRow className="mb-3">
                  <CTable bordered>
                    <CTableHead>
                      <CTableRow>
                        <CTableHeaderCell scope="col">Nombre Del Usuario</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Documento </CTableHeaderCell>
                        <CTableHeaderCell scope="col">Correo Eléctronico</CTableHeaderCell>
                        <CTableHeaderCell scope="col">Estado </CTableHeaderCell>
                        <CTableHeaderCell scope="col">Acciones</CTableHeaderCell>
                      </CTableRow>
                    </CTableHead>
                    <CTableBody>
                      {users &&
                        users.map((user) => (
                          <CTableRow>
                            <CTableDataCell>{user.user.firstName + " " + user.user.lastname}</CTableDataCell>
                            <CTableDataCell>{user.user.document}</CTableDataCell>
                            <CTableDataCell>{user.user.email}</CTableDataCell>
                            <CTableDataCell>{user.user.state}</CTableDataCell>
                            {
                              user.user.state == "Activo" ? (
                                <CTableDataCell><CTooltip content="Editar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => editUser(user.user.idUser)}><CIcon icon={cilPencil} /></CButton></CTooltip> | <CTooltip content="Activar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => changeStateUser(user.user.idUser)}><CIcon icon={cilToggleOn} /></CButton></CTooltip></CTableDataCell>
                              ) : (
                                  <CTableDataCell><CTooltip content="Editar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => editUser(user.user.idUser)}><CIcon icon={cilPencil} /></CButton></CTooltip> | <CTooltip content="Activar" placement="bottom"><CButton color="light" className="rounded-pill" type="button" onClick={() => changeStateUser(user.user.idUser)}><CIcon icon={cilToggleOff} /></CButton></CTooltip></CTableDataCell>
                                )
                            }
                          </CTableRow>
                        ))}
                    </CTableBody>
                  </CTable>
                </CRow>
              </CForm>
            </CCardBody>
          </CCard>
          <CModalFooter>
          </CModalFooter>
        </CModal>
        <div>{modalUpdateUser}</div>
      </CContainer>
    </div>


  )
}

export default AdminUsers
