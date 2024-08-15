//for downloading CSV files

function downloadFile(fileName, base64Data) {
    var link = document.createElement('a');
    link.download = fileName;
    link.href = 'data:text/csv;base64,' + base64Data;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}