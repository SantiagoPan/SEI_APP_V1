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

function OfferProduct1(props) {
  const [departamentos, setDepartamentos] = useState([]);
  const [municipios, setMunicipios] = useState([]);
  const [barrios, setBarrios] = useState([]);
  const [tipoCategoriaProducto, setTipoCategoriaProducto] = useState([]);
  const [categoriaProducto, setCategoriaProducto] = useState([]);
  const [tipoProducto, setTipoProducto] = useState([]);
  const [tipoMaterial, setTipoMaterial] = useState([]);
  const [image, setImage] = useState("");
  const [url, setUrl] = useState("");
  const [dataProducto, setDataProducto] = useState({
    NombreProducto: '',
    Descripcion: '',
    Imagen: '',
    Localizacion: '',
    CostoProducto: 0,
    CostoProductoUnidad: 0,
    Unidades: 0,
    IdTipoCategoriaProducto: '',
    IdCategoriaProducto: '',
    IdTipoProducto: '',
    IdUsuario: ''
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
    Email: '',
    WebSite: ''
  });
  const [dataCaracterizacion, setDataCaracterizacion] = useState({
    Marca: '',
    Modelo: '',
    Condicion: '',
    Ancho: '',
    Alto: '',
    EnvioGratis: '',
    IdTipoMaterialProducto: '',
    IdTipoGarantiaProducto: ''
   
  });

  useEffect(() => {
    axios
      .get("https://localhost:44342/product/getCategoryTypeProduct")
      .then(response => {
        setTipoCategoriaProducto(response.data)
      })
      .catch((error) => {
        console.log(error);
      });

    axios
      .get("https://localhost:44342/product/getTypeMaterialProduct")
      .then(response => {
        setTipoMaterial(response.data)
      })
      .catch((error) => {
        console.log(error);
      });

    axios
      .get("https://localhost:44342/product/getDepartments")
      .then(response => {
        setDepartamentos(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
  },[]);


  const onChangeDepartamento = (e) => {
    let idDepartamento = e.target.value;
    dataLocalizacion.IdDepartamento = e.target.value
    axios
      .get("https://localhost:44342/services/getCities?idDepartamento=" + idDepartamento)
      .then(response => {
        setMunicipios(response.data)
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

  const onChangeTipoCategoriaProducto = (e) => {
    dataProducto.IdTipoCategoriaProducto = e.target.value
    let idTipoCategoriaProducto = e.target.value;
    axios
      .get("https://localhost:44342/product/getProductCategory?idTipoCategoriaProducto=" + idTipoCategoriaProducto)
      .then(response => {
        setCategoriaProducto(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
    categoriaProductoList = categoriaProducto.length > 0
      && categoriaProducto.map((item, i) => {
        return (
          <option key={i} value={item.idCategoriaProducto}>{item.nombre}</option>
        )
      }, this);
  }
  const onChangeCategoriaProducto = (e) => {
    dataProducto.IdCategoriaProducto = e.target.value;
    let idCategoriaProducto = e.target.value;
    axios
      .get("https://localhost:44342/product/getProductType?idCategoriaProducto=" + idCategoriaProducto)
      .then(response => {
        setTipoProducto(response.data)
      })
      .catch((error) => {
        console.log(error);
      });
    tipoProductoList = tipoProducto.length > 0
      && tipoProducto.map((item, i) => {
        return (
          <option key={i} value={item.idTipoProducto}>{item.nombreTiipoProducto}</option>
        )
      }, this);
  }
  const OfferProducts = (e) => {
    const dataImage = new FormData()
    dataImage.append("file", image)
    dataImage.append("upload_preset", "xx3mfqax")
    dataImage.append("cloud_name", "ddsbfi1l5")
    fetch("https://api.cloudinary.com/v1_1/ddsbfi1l5/image/upload", {
      method: "post",
      body: dataImage
    }).then(resp => resp.json())
      .then(data => {
        setUrl(data.url)
        dataProducto.Imagen = data.url;
        var infoUser = JSON.parse(localStorage.getItem('myData'));
        dataProducto.IdUsuario = infoUser.idUser
        e.preventDefault();
        const dataP = {
          caracterizacion: dataCaracterizacion,
          localizacion: dataLocalizacion,
          producto: dataProducto
        };
        const apiUrl = "https://localhost:44342/product/registerProduct";
        axios.post(apiUrl, dataP)
          .then((result) => {
            console.log(result.data);
            const serializedState = JSON.stringify(result.data.UserDetails);
            var a = localStorage.setItem('OfferProducts', serializedState);
            console.log("A:", a)
            const user = result.data.token;
            console.log(user);
            if (result.status == 200)
              window.location.reload(true);
            else
              alert('No registrado');
          })

      }).catch(err => console.log(err))
    debugger;
  };

  const onChange = (e) => {
      e.persist();
      setDataProducto({ ...dataProducto, [e.target.name]: e.target.value });
      setDataLocalizacion({ ...dataLocalizacion, [e.target.name]: e.target.value });
      setDataCaracterizacion({ ...dataCaracterizacion, [e.target.name]: e.target.value });
  }

  let departamentosList = departamentos.length > 0
    && departamentos.map((item, i) => {
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

  let tipoCategoriaProductoList = tipoCategoriaProducto.length > 0
    && tipoCategoriaProducto.map((item, i) => {
      return (
        <option key={i} value={item.idTipoCategoriaProducto}>{item.nombre}</option>
      )
    }, this);

  let tipoMaterialList = tipoMaterial.length > 0
    && tipoMaterial.map((item, i) => {
      return (
        <option key={i} value={item.idTipoMaterialProducto}>{item.nombre}</option>
      )
    }, this);

  let categoriaProductoList = categoriaProducto.length > 0
    && categoriaProducto.map((item, i) => {
      return (
        <option key={i} value={item.idCategoriaProducto}>{item.nombre}</option>
      )
    }, this);

  let tipoProductoList = tipoProducto.length > 0
    && tipoProducto.map((item, i) => {
      return (
        <option key={i} value={item.idTipoProducto}>{item.nombreTiipoProducto}</option>
      )
    }, this);

  return (
    <div className="bg-light min-vh-10 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={9} lg={7} xl={10}>
            <CCard className="mx-4">
              <CCardBody className="p-4">

                <h2>Ofertar Producto</h2>

                <CForm onSubmit={OfferProducts} className="user">
                  <CAlert color="info">Información General</CAlert>
                  <strong>Nombre del Producto</strong>
                  <CInputGroup className="mb-3">
                    <CFormInput value={dataProducto.NombreProducto} onChange={onChange} type="text" className="form-control form-control-sm" id="NombreProducto" name="NombreProducto" />
                  </CInputGroup>

                  <strong>Descripcion Del Producto:</strong>
                  <CInputGroup className="mb-3">
                    <CFormTextarea value={dataProducto.Descripcion} onChange={onChange} type="text" className="form-control form-control-sm" id="Descripcion" name="Descripcion" rows="2" placeholder="Descripcion General Del Producto" ></CFormTextarea>
                  </CInputGroup>

                  <CInputGroup className="mb-3">
                    <CFormInput type="file" onChange={(e) => setImage(e.target.files[0])} accept="image/*" id="Imagen" name="Imagen" />
                  </CInputGroup>

                  <CAlert color="info">Categoria</CAlert>


                  <strong>Tipo de Categoria</strong>
                  <CFormSelect value={dataProducto.IdTipoCategoriaProducto} onChange={onChangeTipoCategoriaProducto} aria-label="Default select example" name="IdTipoCategoria" id="IdTipoCategoria">
                    {tipoCategoriaProductoList}
                  </CFormSelect>

                  <strong>Categoria del Producto</strong>
                  <CFormSelect value={dataProducto.IdCategoriaProducto} onChange={onChangeCategoriaProducto} aria-label="Default select example" name="IdCategoriaProducto" id="IdCategoriaProducto">
                    {categoriaProductoList}
                  </CFormSelect>

                  <strong>Tipo de Producto</strong>
                  <CFormSelect value={dataProducto.IdTipoProducto} onChange={onChange} aria-label="Default select example" name="IdTipoProducto" id="IdTipoProducto">
                    {tipoProductoList}
                  </CFormSelect>
                  <br></br>

                  <CAlert color="info"> Caracterización</CAlert>


                  <strong>Marca</strong>
                  <CInputGroup className="mb-3">
                    <CFormInput value={dataCaracterizacion.Marca} onChange={onChange} type="text" className="form-control form-control-sm" id="Marca" name="Marca" />
                  </CInputGroup>

                  <strong>Condición</strong>
                  <CFormSelect value={dataCaracterizacion.Condicion} onChange={onChange} aria-label="Default select example" name="Condicion" id="Condicion">
                    <option> Seleccione una opción... </option>
                    <option value="Nuevo"> Nuevo</option>
                    <option value="Usado"> Usado</option>
                  </CFormSelect>

                  <strong>Alto(cm)</strong>
                  <CRow className="mb-3">
                    <CCol sm="2">
                      <CFormInput value={dataCaracterizacion.Alto} onChange={onChange} type="text" className="form-control form-control-sm" name="Alto" id="Alto" />
                    </CCol>
                  </CRow>

                  <strong>Ancho(cm)</strong>
                  <CRow className="mb-3">
                    <CCol sm="2">
                    <CFormInput value={dataCaracterizacion.Ancho} onChange={onChange} type="text" className="form-control form-control-sm" id="Ancho" name="Ancho" />
                    </CCol>
                  </CRow>

                  <strong>Material</strong>
                  <CFormSelect value={dataCaracterizacion.IdTipoMaterialProducto} onChange={onChange} aria-label="Default select example" name="IdTipoMaterialProducto" id="IdTipoMaterialProducto">
                    {tipoMaterialList}
                  </CFormSelect>

                  <strong>Envio Gratis</strong>
                  <CFormSelect value={dataCaracterizacion.EnvioGratis} onChange={onChange} aria-label="Default select example" name="EnvioGratis" id="EnvioGratis">
                    <option> Seleccione una opción... </option>
                    <option value="Si">Si</option>
                    <option value="No">No</option>
                  </CFormSelect>

                  <strong> Costo X unidad $:</strong>
                  <CRow className="mb-3">
                  <CCol sm="4">
                      <CFormInput value={dataProducto.CostoProducto} onChange={onChange} type="number" className="form-control form-control-sm" name="CostoProducto" id="CostoProducto" />
                  </CCol>
                  </CRow>

                  <strong>Modelo</strong>
                  <CInputGroup className="mb-3">
                    <CCol sm="4">
                      <CFormInput value={dataCaracterizacion.Modelo} onChange={onChange} type="text" className="form-control form-control-sm" id="Modelo" name="Modelo" />
                    </CCol>
                  </CInputGroup>

                  <strong>Garantia</strong>
                  <CFormSelect value={dataCaracterizacion.IdTipoGarantiaProducto} onChange={onChange} aria-label="Default select example" name="IdTipoGarantiaProducto" id="IdTipoGarantiaProducto">
                    <option> Seleccione una opción... </option>
                    <option value="1">Garantía del vendedor</option>
                    <option value="2">Garantía de fábrica</option>
                    <option value="3">Sin garantía</option>
                  </CFormSelect>

                  <strong>Unidades:</strong>
                  <CInputGroup className="mb-3">
                    <CCol sm="2">
                    <CFormInput value={dataProducto.Unidades} onChange={onChange} type="text" className="form-control form-control-sm" id="Unidades" name="Unidades" />
                      </CCol>
                  </CInputGroup>

                  <CAlert color="info">Localización</CAlert>

                  <strong>Departamento</strong>
                  <CFormSelect value={dataLocalizacion.IdDepartamento} onChange={onChangeDepartamento} aria-label="Default select example" name="IdDepartamento" id="IdDepartamento">
                    {departamentosList}
                  </CFormSelect>

                  <strong>Municipio</strong>
                  <CFormSelect value={dataLocalizacion.IdMunicipio} onChange={onChangeMunicipio} aria-label="Default select example" name="IdMunicipio" id="IdMunicipio">
                    {municipiosList}
                  </CFormSelect>

                  <strong>Barrio</strong>
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
                      <CFormInput value={dataLocalizacion.Telefono} onChange={onChange} type="number" className="form-control form-control-sm" name="Telefono" id="Telefono" />
                    </CCol>
                  </CRow>

                  <strong>Telefono Opcional:</strong>
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

export default OfferProduct1
