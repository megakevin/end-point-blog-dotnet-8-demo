window.addEventListener('DOMContentLoaded', _event => {
    const newFileInput = document.querySelector("#new-quote-image-file");
    newFileInput.addEventListener("change", addNewFileInput);

    let imageInputCount = 0;

    function addNewFileInput(event) {
        convertToSubmittable(event.target)
        createNewFileInput();

        imageInputCount++;
    }

    function convertToSubmittable(fileInput) {
        fileInput.setAttribute("name", "ImageFiles");
        fileInput.setAttribute("id", `ImageFiles_${imageInputCount}`);

        fileInput.removeEventListener("change", addNewFileInput);
    }

    function createNewFileInput() {
        const template = document.querySelector("#quote-image-template");
        const clone = template.content.cloneNode(true);

        let fileInput = clone.querySelector("input");
        fileInput.setAttribute("id", "new-quote-image-file");

        appendToContainer(clone);

        fileInput.addEventListener("change", addNewFileInput);
    }

    function appendToContainer(element) {
        const container = document.querySelector("#quote-images-container");
        container.appendChild(element);
    }
});
