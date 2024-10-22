function exportTablesToExcel(tableIDs, filename = ''){
    debugger
    const wb = XLSX.utils.book_new();
    const tablesHTML = Object.keys(tableIDs).map((id, index) => {
        const ws = XLSX.utils.table_to_sheet(document.getElementById(id));
        XLSX.utils.book_append_sheet(wb, ws, "Sheet"+(index+1));
    });
    XLSX.writeFile(wb, filename?filename+'.xlsx':'excel_data.xlsx');
}

function exportTablesToExcelFromJson(tableIDs, filename = ''){
    debugger
    try {
        const IDs = JSON.parse(tableIDs)
        const wb = XLSX.utils.book_new();
        const keys = Object.keys(IDs);
        for (const key of keys){
            const table = document.getElementById(key);
            const ws = XLSX.utils.table_to_sheet(table);
            XLSX.utils.book_append_sheet(wb, ws, "Sheet"+(keys.indexOf(key)));
        }
    // keys.map((id, index) => {
    //     const ws = XLSX.utils.table_to_sheet(document.getElementById(id));
    //     XLSX.utils.book_append_sheet(wb, ws, "Sheet"+(index+1));
    // });
        XLSX.writeFile(wb, filename?filename+'.xlsx':'excel_data.xlsx');
    }catch (error){
        console.error(error)
    }
}

function downloadJsonFile(filename, content) {
    const element = document.createElement('a');
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(content));
    element.setAttribute('download', filename);

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
}
