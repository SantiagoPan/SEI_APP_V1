import React, { useState, useEffect} from 'react';
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
import CIcon from '@coreui/icons-react'
import { cilCart, cilSearch, cilZoom } from '@coreui/icons'
import { CModalContent } from '@coreui/react-pro';


function SearchProduct1(props) {
  const [departamentos, setDepartamentos] = useState([]);
  const [municipios, setMunicipios] = useState([]);
  const [barrios, setBarrios] = useState([]);
  const [tipoPago, setTipoPago] = useState([]);
  const [visibleBuy, setVisibleBuy] = useState(false)
  const [visibleDet, setVisibleDet] = useState(false)
  const [products, setDataProducts] = useState([]);
  const [busqueda, setBusqueda] = useState("");
  const [dataProducto, setDataProducto] = useState({
    IdProducto: '',
    Descripcion: '',
    Imagen: '',
    Localizacion: '',
    CostoTotal: 0,
    Unidades: 0,
    Unidades: 0,
    EnvioGratis: 0,
    IdTipoPago: '',
  });

  const [dataCompra, setDataCompra] = useState({
    UnidadesCompradas: 0,
    IdProducto: 0,
    IdTipoPago: 0,
    IdUsuarioComprador: ''
  });

  const [dataEnvio, setDataEnvio] = useState({
    NombreCompradors: '',
    Telefono: '',
    CorreoElectronico: '',
    DireccionEnvio: '',
    DatosAdicionales: '',
    EnvioGratis: '',
    IdMunicipio: 0,
    IdBarrio: 0,

  });

  const GetProductos = async () => {
    await axios.get("https://localhost:44342/product/getProducts")
        .then(response => {
          setDataProducts(response.data)
        })
        .catch((error) => {
          console.log(error);
        });
  }
  const GetDepartamentos = async () => {
    axios
      .get("https://localhost:44342/product/getDepartments")
      .then(response => {
        setDepartamentos(response.data)
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
    GetDepartamentos();
    GetProductos();
    GetTipoPago();
  }, [])


  const onChangeDepartamento = (e) => {
    let idDepartamento = e.target.value;
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
    dataEnvio.IdMunicipio = e.target.value
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

  const BuyProduct = (e) => {
    e.preventDefault();
    var infoUser = JSON.parse(localStorage.getItem('myData'));
    dataCompra.IdUsuarioComprador = infoUser.idUser
    const data = {
      envio: dataEnvio,
      producto: dataCompra
    };
    const apiUrl = "https://localhost:44342/product/buyProduct";
    axios.post(apiUrl, data)
      .then((result) => {
        debugger;
        console.log(result.data);
        const serializedState = JSON.stringify(result.data.UserDetails);
        var a = localStorage.setItem('BuyProducts', serializedState);
        console.log("A:", a)
        const user = result.data.token;
        console.log(user);
        if (result.status == 200)
          window.location.reload(true);
        else
          alert('No registrado');
      })
  };

  let tipoPagoList = tipoPago.length > 0
    && tipoPago.map((item, i) => {
      return (
        <option key={i} value={item.idTipoPago}>{item.nombre}</option>
      )
    }, this);

  const filtrar = (terminoBusqueda) => {
    var resultadosBusqueda = products.filter((elemento) => {
      if (elemento.nombreProducto.toString().toLowerCase().includes(terminoBusqueda.toLowerCase())
        | elemento.nombreProducto.toString().toLowerCase().includes(terminoBusqueda.toLowerCase())
      ) {
        return elemento;
      }
    });
    setUsuarios(resultadosBusqueda);
  }
  const handleChange = e => {
    setBusqueda(e.target.value);
    filtrar(e.target.value);
  }

  const showModalBuy = () => {
    setVisibleBuy(!visibleBuy)
  };

  const showModalDetails = () => {
    setVisibleDet(!visibleDet)
  };

  const onChange = (e) => {
    e.persist();
    setDataCompra({ ...dataCompra, [e.target.name]: e.target.value });
  }
  const onChangeEnvio = (e) => {
    e.persist();
    setDataEnvio({ ...dataEnvio, [e.target.name]: e.target.value });
  }

  return (
    <div className="bg-light min-vh-10 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={9} lg={7} xl={12}>
            <CCard className="mx-4">
              <CCardBody className="p-4">
                <CForm className="user">
                  <h2>Productos Disponibles</h2>
                  {/*<div>*/}
                  {/*  <CInputGroup className="input-prepend">*/}
                  {/*    <CInputGroupText>*/}
                  {/*      <CIcon icon={cilSearch} />*/}
                  {/*    </CInputGroupText>*/}
                  {/*    <CFormInput type="text" value={busqueda} id="strSearch" name="strSearch" onChange={handleChange} />*/}
                  {/*    <CButton color="primary" type="submit">Buscar Producto</CButton>*/}
                  {/*  </CInputGroup>*/}
                  {/*</div>*/}
                </CForm>
                <br>
                </br>
                <div className="row">
                {products &&
                    products.map((producto) => (
                    <CCard style={{ width: '18rem' }} key={producto.producto.idProducto}>
                      <CCardImage orientation="top" src={producto.producto.imagen} />
                      <CCardBody>
                          <CCardTitle>{producto.producto.nombreProducto} | <small>{producto.producto.caracteristicas.condicion}</small></CCardTitle>
                        <br></br> 
                          <CCardText><strong>Calificación: </strong>
                          <small> {producto.producto.estrellasCalificacion}</small></CCardText>
                        <CCardText><strong>Disponibles en stock: </strong> <small> {producto.producto.unidades}</small></CCardText>
                        <CCardText><strong>Precio: $</strong> {producto.producto.costoProducto}</CCardText>
                        <div className="d-grid gap-2 d-md-block">
                          <CButton href="" color="primary" onClick={showModalBuy}><CIcon icon={cilCart} />Comprar </CButton>
                          <CButton color="warning" onClick={showModalDetails}><CIcon icon={cilZoom} />Ver Detalles </CButton>
                        </div>
      
                    </CCardBody>
                  </CCard>
                    ))}

                  {products &&
                    products.map((producto) => (
                  <CModal visible={visibleBuy} onClose={() => setVisibleBuy(false)}>
                    <CModalHeader onClose={() => setVisibleBuy(false)}>
                          <CModalTitle>Datos del producto {producto.producto.nombreProducto}</CModalTitle>
                    </CModalHeader>
                        <CCard className="mx-6" key={producto.producto.idProducto}>
                          <CCardBody className="p-6">
                            <CAlert color="info">Datos de envio</CAlert>
                            <CForm onSubmit={BuyProduct} className="row g-3">
                              <CFormInput type="hidden" value={dataCompra.idProducto = producto.producto.idProducto} className="form-control form-control-sm" name="IdProducto" id="IdProducto" />
                              <CCol md={7}>
                                <CFormInput type="text" onChange={onChangeEnvio} placeholder="Nombre Completo" id="NombreCompradors" name="NombreCompradors" label="NombreCompradors" />
                              </CCol>
                              <CCol md={5}>
                                <CFormInput type="text" onChange={onChangeEnvio} placeholder="Teléfono" id="Telefono" name="Telefono" label="Telefono" />
                              </CCol>
                              <CCol xs={12}>
                                <CFormInput onChange={onChangeEnvio} type="email" placeholder="Email" id="CorreoElectronico"  name="CorreoElectronico" label="CorreoElectronico" />
                              </CCol>
                              <CCol xs={12}>
                                <CFormInput onChange={onChangeEnvio} label="DireccionEnvio" id="DireccionEnvio" name="DireccionEnvio" placeholder="Dirección" />
                              </CCol>
                              <CCol xs={12}>
                                <CFormInput onChange={onChangeEnvio} id="DatosAdicionales" name="DatosAdicionales" label="DatosAdicionales" placeholder="Datos Adicionales" />
                              </CCol>
                              <CCol md={4}>
                                <CFormLabel htmlFor="IdDepartamento" className="col-sm-10 col-form-label">Departamento</CFormLabel>
                                <CFormSelect  onChange={onChangeDepartamento} aria-label="Default select example" name="IdDepartamento" id="IdDepartamento">
                                  {departamentosList}
                                </CFormSelect>
                              </CCol>
                              <CCol md={4}>
                                <CFormLabel htmlFor="IdMunicipio" className="col-sm-10 col-form-label">Ciudad</CFormLabel>
                                <CFormSelect value={dataEnvio.IdMunicipio} onChange={onChangeMunicipio} aria-label="Default select example" name="IdMunicipio" id="IdMunicipio">
                                  {municipiosList}
                                </CFormSelect>
                              </CCol>
                              <CCol md={4}>
                                <CFormLabel htmlFor="IdBarrio" className="col-sm-10 col-form-label">Barrio</CFormLabel>
                                <CFormSelect value={dataEnvio.IdBarrio} onChange={onChangeEnvio} aria-label="Default select example" name="IdBarrio" id="IdBarrio">
                                  {barriosList}
                                </CFormSelect>
                              </CCol>

                              <CCol xs={12}>
                                <CCardText>El envio de este producto <strong> {producto.producto.caracteristicas.envioGratis} </strong> es gratis</CCardText>
                              </CCol>

                              <CAlert color="info">Datos de la compra</CAlert>
                              <CCol md={6}>
                                <CFormLabel htmlFor="IdMunicipio" className="col-sm-10 col-form-label">Tipo de pago</CFormLabel>
                                <CFormSelect value={dataCompra.IdTipoPago} onChange={onChange} aria-label="Default select example" name="IdTipoPago" id="IdTipoPago" label="Tipo de pago">
                                  {tipoPagoList}
                                </CFormSelect>
                              </CCol>

                              <CCol md={6}>
                                <CFormLabel htmlFor="UnidadesCompradas" className="col-sm-10 col-form-label">Unidades</CFormLabel>
                                <CFormInput required type="number" min="1" max={producto.producto.unidades} value={dataCompra.UnidadesCompradas} onChange={onChange} className="form-control form-control-sm" name="UnidadesCompradas" id="UnidadesCompradas" />
                              </CCol>

                              <CCol md={6}>
                                <CFormLabel htmlFor="ValorPorUnidad" className="col-sm-10 col-form-label">Valor Unidad</CFormLabel>
                                <CFormInput disabled type="number" value={producto.producto.costoProducto} onChange={onChange} className="form-control form-control-sm" name="ValorPorUnidad" id="ValorPorUnidad" />
                              </CCol>
           
                              <CCol md={6}>
                                <CFormLabel htmlFor="CostoTotal" className="col-sm-10 col-form-label">Total a pagar:</CFormLabel>
                                <CFormInput disabled type="number" value={producto.producto.costoProducto * dataCompra.UnidadesCompradas} onChange={onChange} className="form-control form-control-sm" name="CostoTotal" id="CostoTotal" />
                              </CCol>

                              <CCol md={6}>
                                <CButton type="submit" color="success">Comprar</CButton>
                              </CCol>
                            </CForm>
                      </CCardBody>
                    </CCard>
                    <CModalFooter>

                    </CModalFooter>
                  </CModal>
                    ))}

                  {products &&
                    products.map((producto) => (
                      <CModal visible={visibleDet} onClose={() => setVisibleDet(false)}>
                        <CModalHeader onClose={() => setVisibleDet(false)}>
                          <CModalTitle>Detalles del producto - {producto.producto.nombreProducto}</CModalTitle>
                        </CModalHeader>
                        <CCard className="mx-4" key={producto.producto.idProducto}>
                          <CCardBody className="p-4">
                            <CForm className="user">
                              <CRow className="mb-3">
                                <CCardSubtitle><strong>Descripción</strong> </CCardSubtitle>
                                <CCardText>{producto.producto.descripcion}</CCardText>
                                <CCardText><strong>Condición: </strong>{producto.producto.caracteristicas.condicion}</CCardText>
                                <CCardText><strong>Categoria: </strong>{producto.producto.nombreTipoProducto}</CCardText>
                                <CCardText><strong>Marca: </strong>{producto.producto.caracteristicas.marca}<strong> Modelo: </strong>{producto.producto.caracteristicas.modelo}</CCardText>
                                <CCardText><strong>Material: </strong>{producto.producto.caracteristicas.material}</CCardText>
                                <CCardText><strong>Dimenciones: </strong>Alto {producto.producto.caracteristicas.alto} cm x {producto.producto.caracteristicas.ancho} cm de ancho</CCardText>
                                <CCardText><strong>Garantia: </strong>{producto.producto.caracteristicas.garantia}</CCardText>
                                <CCardText><strong>Ubicación: </strong>{producto.producto.localizacion.ciudad}, {producto.producto.localizacion.departmento} <b> Barrio: </b> {producto.producto.localizacion.barrio}</CCardText>
                              </CRow>
                            </CForm>
                          </CCardBody>
                        </CCard>
                        <CModalFooter>
                        </CModalFooter>
                      </CModal>
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

export default SearchProduct1
