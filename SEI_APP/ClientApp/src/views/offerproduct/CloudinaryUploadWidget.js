import React, { Component } from "react";
import cloudinary from "cloudinary";

class CloudinaryUploadWidget extends Component {
  componentDidMount() {
    const widgetfile = window.cloudinary.createUploadWidget(
      {
        cloudName: "ddsbfi1l5",
        uploadPreset: "HbxJ7l2yjGjfoATI3dObn3Jk6Jo"
      },
      (error, result) => {
        if (!error && result && result.event === "success") {
          console.log("Done! Here is the image info: ", result.info);
        }
      }
    );
    document.getElementById("upload_widget").addEventListener(
      "click",
      function () {
        widgetfile.open();
      },
      false
    );
  }

  render() {
    return (
      <button id="upload_widget" className="cloudinary-button">
        Upload
      </button>

    );
  }
}

export default CloudinaryUploadWidget;
