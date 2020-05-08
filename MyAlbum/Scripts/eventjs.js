function ondrop(event) {
    alert('ddd');
    event.stopPropagation();
    event.preventDefault();
    var dt = event.dataTransfer;
    var files = dt.files;

}

