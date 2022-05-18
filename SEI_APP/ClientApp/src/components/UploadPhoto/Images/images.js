import React, { Component } from "react";
import FileUploader from "./FileUploader";
import axios from "axios";

export default class images extends Component {
  uploader = React.createRef();
  constructor(props) {
    super(props);
    this.onClick = this.onClick.bind(this);
  }

  onClick(e) {
    e.preventDefault();

    var url =
      "https://apiempleadosier.azurewebsites.net/api/Prueba/InsertarPrueba",
      formData = new FormData(document.getElementById("form"));
    var urlimagen =
      "https://apiempleadosier.azurewebsites.net/Imagen/SubirImagen";
    console.log(this.uploader.current);
    if (undefined === this.uploader.current) return;

    formData.append("file", this.uploader.current.getFileObject());
    var json = {
      idPrueba: 4,
      imagen: this.uploader.current.getFileName(),
      texto: "Hola que tal"
    };
    var jsonimagen = { imagenRaw: this.uploader.current.getFileObject() };

    axios.post(url, json).then(console.log("Guay"));
    //axios
    //.post(urlimagen, this.uploader.current.getFileObject())
    //.then(console.log("Guayx2"));
  }

  render() {
    return (
      <div>
        <FileUploader ref={this.uploader} />
        <button type="submit" onClick={this.onClick}>
          Enviar
        </button>
      </div>
    );
  }
}
