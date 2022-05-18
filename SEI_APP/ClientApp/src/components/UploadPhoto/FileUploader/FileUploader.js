import React, { Component } from "react";

export default class FileUploader extends Component {
  form = React.createRef();
  state = {
    active: false,
    imageSrc: "",
    imageName: "",
    image: [],
    loaded: false
  };
  constructor(props) {
    super(props);

    this.state = {
      active: false,
      imageSrc: "",
      imageName: "",
      image: [],
      loaded: false
    };

    this.onDragEnter = this.onDragEnter.bind(this);
    this.onDragLeave = this.onDragLeave.bind(this);
    this.onDrop = this.onDrop.bind(this);
    this.onFileChange = this.onFileChange.bind(this);
  }

  onDragEnter(e) {
    this.setState({ active: true });
  }

  onDragLeave(e) {
    this.setState({ active: false });
  }

  onDragOver(e) {
    e.preventDefault();
  }

  onDrop(e) {
    e.preventDefault();
    this.setState({ active: false });
    this.onFileChange(e, e.dataTransfer.files[0]);
  }

  onFileChange(e, file) {
    console.log("on-file-change");
    var file = file || e.target.files[0],
      pattern = /image-*/,
      reader = new FileReader();
    console.log(file.name);
    if (!file.type.match(pattern)) {
      alert("Formato inválido");
      return;
    }
    console.log(this);
    this.setState({ loaded: false });

    reader.onload = e => {
      this.setState({
        imageSrc: reader.result,
        loaded: true,
        imageName: file.name
      });
      console.log(reader.result);
    };
    reader.readAsDataURL(file);
  }

  getFileName() {
    return this.state.imageName;
  }
  getFileObject() {
    return this.refs.input.files[0];
  }

  getFileString() {
    return this.state.imageSrc;
  }

  render() {
    let state = this.state,
      props = this.props;
    return (
      <form name="form" ref={this.formRef} id="form">
        <label
          onDragEnter={this.onDragEnter}
          onDragLeave={this.onDragLeave}
          onDragOver={this.onDragOver}
          onDrop={this.onDrop}
        >
          <img src={state.imageSrc} />
          <i className="icon icon-upload"></i>
          <input
            type="file"
            accept="image/*"
            onChange={this.onFileChange}
            ref="input"
          />
        </label>
      </form>
    );
  }
}
