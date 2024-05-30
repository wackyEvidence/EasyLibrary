import { saveAs } from 'file-saver';
import * as XLSX from 'xlsx';

export const exportToExcel = (data, fileName) => {
    // Создаем новый рабочий лист (worksheet) из массива данных
    const ws = XLSX.utils.json_to_sheet(data);

    // Создаем новый рабочий стол (workbook) и добавляем в него рабочий лист
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Пользователи');

    // Генерируем файл Excel и сохраняем его
    const excelBuffer = XLSX.write(wb, { bookType: 'xlsx', type: 'array' });
    const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
    saveAs(blob, `${fileName}.xlsx`);
};
