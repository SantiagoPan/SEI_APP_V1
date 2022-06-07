import {
  CAlert, CButton,
  CCard,
  CCardBody,
  CCol,
  CContainer,
  CForm,
  CFormInput, CFormLabel, CFormSelect, CFormTextarea, CInputGroup, CInputGroupText, CRow
} from '@coreui/react';
import axios from 'axios';
import React, { useState, useEffect } from 'react';

function OfferService1(props) {
  const [departamentos, setDepartamentos] = useState([]);
  const [municipios, setMunicipios] = useState([]);
  const [barrios, setBarrios] = useState([]);
  const [tipoCategoriaServicio, setTipoCategoriaServicio] = useState([]);
  const [categoriaServicio, setCategoriaServicio] = useState([]);
  const [image, setImage] = useState("");
  const [url, setUrl] = useState("");
  const [tipoServicio, setTipoServicio] = useState([]);
  const [dataServicio, setDataServicio] = useState({
      NombreServicio: '',
      IdUsuario: '',
      Descripcion: '',
      Imagen: '',
      Localizacion: '',
      CostoServicio: 0,
      AplicaConvenio:'',
      IdTipoCategoriaServicio: '',
      IdCategoriaServicio: '',
      IdTipoServicio: '',
    });
  const [dataLocalizacion, setDataLocalizacion] = useState({
      Direccion: '',
      CodigoPostal: '',
      DatosAdicionales: '',
      IdDepartamento: '',
      IdMunicipio: '',
      IdBarrio: '',
      Telefono: '',
      TelefonOpc: '',
      Correo: '',
      WebSite: ''
    });
  const [dataCaracterizacion, setDataCaracterizacion] = useState({
      Experiencia: 0,
      Incluye: '',
      NoIncluye: '',
  });


  useEffect(() => {
    axios
      .get("https://localhost:44342/services/getCategoryTypeService")
      .then(response => {
        setTipoCategoriaServicio(response.data)
        setTimeout(() => {
          console.log(tipoCategoriaServicio);
        }, 3000)
      })
      .catch((error) => {
        console.log(error);
      });

    axios
      .get("https://localhost:44342/product/getDepartments")
      .then(response => {
        setDepartamentos(response.data)
        setTimeout(() => {
          console.log(departamentos);
        }, 3000)
      })
      .catch((error) => {
        console.log(error);
      });



  }, []);

  const onChangeDepartamento = (e) => {
    let idDepartamento = e.target.value;
    dataLocalizacion.IdDepartamento = e.target.value
    axios
      .get("https://localhost:44342/services/getCities?idDepartamento=" + idDepartamento)
      .then(response => {
        setMunicipios(response.data)
        setTimeout(() => {
          console.log(municipios);
        }, 3000)
      })
      .catch((error) => {
        console.log(error);
      });

    municipiosList = municipios.length > 0
      && municipios.map((item, i) => {
        return (
          <option key={i} value={item.idMunicipio}>{item.nombre}</option>
        )
      }, this);

  }
  const onChangeMunicipio = (e) => {
    let idMunicipio = e.target.value;
    dataLocalizacion.IdMunicipio = e.target.value
    axios
      .get("https://localhost:44342/services/getNighborhoods?idMunicipio=" + idMunicipio)
      .then(response => {
        setBarrios(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
    barriosList = barrios.length > 0
      && barrios.map((item, i) => {
        return (
          <option key={i} value={item.idBarrio}>{item.nombre}</option>
        )
      }, this);
  }

  const onChangeTipoCategoriaServicio = (e) => {
    dataServicio.IdTipoCategoriaServicio = e.target.value
    let idTipoCategoriaServicio = e.target.value;
    axios
      .get("https://localhost:44342/services/getServiceCategory?idTipoCategoriaServicio=" + idTipoCategoriaServicio)
      .then(response => {
        setCategoriaServicio(response.data)
      })  
      .catch((error) => {
        console.log(error);
      });
    categoriaServicioList = categoriaServicio.length > 0
      && categoriaServicio.map((item, i) => {
        return (
          <option key={i} value={item.idCategoriaServicio}>{item.nombre}</option>
        )
      }, this);
  }

  const onChangeCategoriaServicio = (e) => {
    dataServicio.IdCategoriaServicio =  e.target.value;
    let idCategoriaServicio = e.target.value;
    axios
      .get("https://localhost:44342/services/getServiceType?idCategoriaServicio=" + idCategoriaServicio)
      .then(response => {
        setTipoServicio(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
     tipoServicioList = tipoServicio.length > 0
      && tipoServicio.map((item, i) => {
        return (
          <option key={i} value={item.idTipoServicio}>{item.nombre}</option>
        )
      }, this);
  }
  const SaveService = (e) => {
    const dataImage = new FormData()
    dataImage.append("file", image)
    dataImage.append("upload_preset", "xx3mfqax")
    dataImage.append("cloud_name", "ddsbfi1l5")
    fetch("https://api.cloudinary.com/v1_1/ddsbfi1l5/image/upload", {
      method: "post",
      body: dataImage
    })
      .then(resp => resp.json())
      .then(data => {
        setUrl(data.url)
        dataServicio.Imagen = data.url;
        e.preventDefault();
        var infoUser = JSON.parse(localStorage.getItem('myData'));
        dataServicio.IdUsuario = infoUser.idUser
        const dataS = {
          caracterizacion: dataCaracterizacion,
          localizacion: dataLocalizacion,
          servicio: dataServicio
        };
        const apiUrl = "https://localhost:44342/services/registerService";
        axios.post(apiUrl, dataS)
          .then((result) => {
            debugger;
            console.log(result.data);
            const serializedState = JSON.stringify(result.data.UserDetails);
            var a = localStorage.setItem('OfferServiceInfo', serializedState);
            console.log("A:", a)
            const user = result.data.token;
            console.log(user);
            if (result.status == 200)
              window.location.reload(true);
            else
              alert('No registrado');
          })

      })
      .catch(err => console.log(err))

  };

  const onChange = (e) => {
    e.persist();
    setDataServicio({ ...dataServicio, [e.target.name]: e.target.value });
    setDataLocalizacion({ ...dataLocalizacion, [e.target.name]: e.target.value });
    setDataCaracterizacion({ ...dataCaracterizacion, [e.target.name]: e.target.value });
  }

  let departamentosList = departamentos.length > 0
    && departamentos.map((item, i) => {
      const optionsDep = <option key={i} value={item.idDepartamento}>{item.nombre}</option>;
      return (
        <option key={i} value={item.idDepartamento}>{item.nombre}</option>
      )
    }, this);

  let municipiosList = municipios.length > 0
    && municipios.map((item, i) => {
      return (
        <option key={i} value={item.idMunicipio}>{item.nombre}</option>
      )
    }, this);

  let barriosList = barrios.length > 0
    && barrios.map((item, i) => {
      return (
        <option key={i} value={item.idBarrio}>{item.nombre}</option>
      )
    }, this);

  let tipoCategoriaServicioList = tipoCategoriaServicio.length > 0
    && tipoCategoriaServicio.map((item, i) => {
      return (
        <option key={i} value={item.idTipoCategoriaServicio}>{item.nombre}</option>
      )
    }, this);

  let categoriaServicioList = categoriaServicio.length > 0
    && categoriaServicio.map((item, i) => {
      return (
        <option key={i} value={item.idCategoriaServicio}>{item.nombre}</option>
      )
    }, this);

  let tipoServicioList = tipoServicio.length > 0
    && tipoServicio.map((item, i) => {
      return (
        <option key={i} value={item.idTipoServicio}>{item.nombre}</option>
      )
    }, this);

    return (
      <div className="bg-light min-vh-10 d-flex flex-row align-items-center">
        <CContainer>
          <CRow className="justify-content-center">
            <CCol md={9} lg={7} xl={10}>
              <CCard className="mx-4">
                <CCardBody className="p-4">

                  <h2>Ofertar Servicios</h2>

                  <CForm onSubmit={SaveService} className="user">
                    <CAlert color="primary">
                      Información General
                    </CAlert>

                    <strong>Nombre Del Servicio:</strong>
                    <CInputGroup className="mb-3">
                      <CFormInput value={dataServicio.NombreServicio} onChange={onChange} type="text" className="form-control form-control-sm" id="NombreServicio" name="NombreServicio" />
                    </CInputGroup>

                    <strong>Descripción Del Servicio:</strong>
                    <CInputGroup className="mb-3">
                      <CFormTextarea value={dataServicio.Descripcion} onChange={onChange} type="text" className="form-control form-control-sm" id="Descripcion" name="Descripcion" rows="2" placeholder="Descripción General Del Servicio" ></CFormTextarea>
                    </CInputGroup>


                    <CInputGroup className="mb-3">
                      <CFormInput type="file" onChange={(e) => setImage(e.target.files[0])} accept="image/*" id="Imagen" name="Imagen" />
                    </CInputGroup>

                    <CAlert color="primary">Categoría</CAlert>

                    <strong>Tipo de Categoria</strong>
                    <CFormSelect value={dataServicio.IdTipoCategoriaServicio} onChange={onChangeTipoCategoriaServicio} aria-label="Default select example" name="IdTipoCategoriaServicio" id="IdTipoCategoriaServicio">
                      {tipoCategoriaServicioList}
                    </CFormSelect>

                    <strong>Categoría</strong>
                    <CFormSelect value={dataServicio.IdCategoriaServicio} onChange={onChangeCategoriaServicio} aria-label="Default select example" name="IdCategoriaServicio" id="IdCategoriaServicio">
                      {categoriaServicioList}
                    </CFormSelect>

                    <strong>Tipo de Servicio</strong>
                    <CFormSelect value={dataServicio.IdTipoServicio} onChange={onChange} aria-label="Default select example" name="IdTipoServicio" id="IdTipoServicio">
                      {tipoServicioList}
                    </CFormSelect>
                    <br></br>
                    <CAlert color="primary"> Caracterización</CAlert>

                    <CRow className="mb-3">
                      <CFormLabel htmlFor="colFormLabelSm" ><strong>Años De Experiencia:</strong></CFormLabel>
                      <CCol sm="2">
                        <CFormInput value={dataCaracterizacion.Experiencia} onChange={onChange} type="number" className="form-control form-control-sm" name="Experiencia" id="Experiencia" />
                      </CCol>
                    </CRow>

                    <strong>El Servicio Incluye:</strong>
                    <CInputGroup className="mb-3">
                      <CFormTextarea value={dataCaracterizacion.Incluye} onChange={onChange} rows="2" name="Incluye" id="Incluye"></CFormTextarea>
                    </CInputGroup>

                    <strong>El Servicio No Incluye: </strong>
                    <CInputGroup className="mb-3">
                      <CFormTextarea value={dataCaracterizacion.NoIncluye} onChange={onChange} rows="2" name="NoIncluye" id="NoIncluye"></CFormTextarea>
                    </CInputGroup>

                    <strong> Costo:</strong>
                    <CRow className="mb-3">
                      <CCol sm="4">
                        <CFormInput alue={dataServicio.CostoServicio} onChange={onChange} type="number" className="form-control form-control-sm" name="CostoServicio" id="CostoServicio"/>
                      </CCol>
                      <strong> A convenir con el cliente:</strong>
                      <CFormSelect value={dataServicio.AplicaConvenio} onChange={onChange} aria-label="Default select example" name="AplicaConvenio" id="AplicaConvenio">
                        <option>Seleccione...</option>
                        <option value="Si">Si</option>
                        <option value="No">No</option>
                      </CFormSelect>
                    </CRow>

                    <CAlert color="primary">Localización</CAlert>

                    <strong>Departamentos</strong>
                    <CFormSelect value={dataLocalizacion.IdDepartamento} onChange={onChangeDepartamento} aria-label="Default select example" name="IdDepartamento" id="IdDepartamento">
                      {departamentosList}
                    </CFormSelect>
                      
                    <strong>Municipios</strong>
                    <CFormSelect value={dataLocalizacion.IdMunicipio} onChange={onChangeMunicipio} aria-label="Default select example" name="IdMunicipio" id="IdMunicipio">
                      {municipiosList}
                    </CFormSelect>

                    <strong>Barrios</strong>
                    <CFormSelect value={dataLocalizacion.IdBarrio} onChange={onChange} aria-label="Default select example" name="IdBarrio" id="IdBarrio">
                      {barriosList}
                    </CFormSelect>

                    <strong>Codigo Postal</strong>
                    <CFormSelect value={dataLocalizacion.CodigoPostal} onChange={onChange} aria-label="Default select example" name="CodigoPostal" id="CodigoPostal">
                      <option>Seleccione...</option>
                      <option value="54840">54840</option>
                      <option value="84815">54800</option>
                    </CFormSelect>

                    <strong>Direccion: </strong>
                    <CRow className="mb-3">
                      <CCol sm={10} >
                        <CFormInput value={dataLocalizacion.Direccion} onChange={onChange} type="text" className="form-control form-control-sm" name="Direccion" id="Direccion" />
                      </CCol>
                    </CRow>

                    <strong>Datos adicionales: </strong>
                    <CRow className="mb-3">
                      <CCol sm={10} >
                        <CFormInput value={dataLocalizacion.DatosAdicionales} onChange={onChange} type="text" className="form-control form-control-sm" name="DatosAdicionales" id="DatosAdicionales" />
                      </CCol>
                    </CRow>

                    <strong>Telefono Personal:</strong>
                    <CRow className="mb-3">
                      <CCol sm={10} >
                        <CFormInput value={dataLocalizacion.Telefono} onChange={onChange} type="number" className="form-control form-control-sm" name="Telefono" id="Telefono"/>
                      </CCol>
                    </CRow>

                    <strong>Telefono Adicional: </strong>
                    <CRow className="mb-3">
                      <CCol sm={10} >
                        <CFormInput value={dataLocalizacion.TelefonOpc} onChange={onChange} type="number" className="form-control form-control-sm" name="TelefonOpc" id="TelefonOpc" />
                      </CCol>
                    </CRow>

                    <strong>Correo:</strong>
                    <CRow className="mb-3">
                      <CCol sm={10} >
                        <CFormInput value={dataLocalizacion.Email} onChange={onChange} type="email" className="form-control form-control-sm" name="Email" id="Email" />
                      </CCol>
                    </CRow>

                    <strong>Sitios Web </strong>
                    <CRow className="mb-3">
                      <CCol sm={10} >
                        <CFormInput alue={dataLocalizacion.WebSite} onChange={onChange} type="text" className="form-control form-control-sm" name="WebSite" id="WebSite" />
                      </CCol>
                    </CRow>
                    <div className="d-grid">
                      <CButton color="success" type="submit" > Crear Oferta</CButton>
                    </div>
                    <div className="d-grid">
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

export default OfferService1;
