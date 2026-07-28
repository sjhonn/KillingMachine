const menuButton = document.querySelector('[data-menu-toggle]');
const mainNav = document.querySelector('[data-main-nav]');
if (menuButton && mainNav) {
    menuButton.addEventListener('click', () => {
        const open = mainNav.classList.toggle('open');
        menuButton.setAttribute('aria-expanded', String(open));
    });
}

document.querySelectorAll('.flash').forEach((item) => {
    window.setTimeout(() => item.classList.add('fade'), 5000);
});

function prepareCanvas(id) {
    const canvas = document.getElementById(id);
    if (!canvas) return null;
    const ratio = window.devicePixelRatio || 1;
    const width = canvas.clientWidth || 600;
    const height = Number(canvas.getAttribute('height')) || 260;
    canvas.width = width * ratio;
    canvas.height = height * ratio;
    canvas.style.height = `${height}px`;
    const ctx = canvas.getContext('2d');
    ctx.scale(ratio, ratio);
    return { canvas, ctx, width, height };
}

function drawAxes(ctx, width, height) {
    ctx.strokeStyle = '#d8d8d8';
    ctx.lineWidth = 1;
    ctx.beginPath();
    ctx.moveTo(45, 20);
    ctx.lineTo(45, height - 35);
    ctx.lineTo(width - 15, height - 35);
    ctx.stroke();
}

function drawLineChart(id, labels, values, unit) {
    const chart = prepareCanvas(id);
    if (!chart || !values || values.length === 0) return;
    const { ctx, width, height } = chart;
    drawAxes(ctx, width, height);
    const min = Math.min(...values) - 2;
    const max = Math.max(...values) + 2;
    const range = Math.max(max - min, 1);
    const usableWidth = width - 75;
    const usableHeight = height - 70;
    ctx.strokeStyle = '#111';
    ctx.fillStyle = '#111';
    ctx.lineWidth = 3;
    ctx.beginPath();
    values.forEach((value, index) => {
        const x = 45 + (values.length === 1 ? usableWidth / 2 : (index * usableWidth / (values.length - 1)));
        const y = 20 + (max - value) * usableHeight / range;
        if (index === 0) ctx.moveTo(x, y); else ctx.lineTo(x, y);
    });
    ctx.stroke();
    ctx.font = '12px Arial';
    values.forEach((value, index) => {
        const x = 45 + (values.length === 1 ? usableWidth / 2 : (index * usableWidth / (values.length - 1)));
        const y = 20 + (max - value) * usableHeight / range;
        ctx.beginPath(); ctx.arc(x, y, 4, 0, Math.PI * 2); ctx.fill();
        ctx.fillText(`${value} ${unit}`, x - 18, y - 10);
        ctx.fillText(labels[index] || '', x - 14, height - 15);
    });
}

function drawBarChart(id, labels, values, unit) {
    const chart = prepareCanvas(id);
    if (!chart || !values || values.length === 0) return;
    const { ctx, width, height } = chart;
    drawAxes(ctx, width, height);
    const max = Math.max(...values, 1);
    const usableWidth = width - 75;
    const usableHeight = height - 70;
    const slot = usableWidth / values.length;
    ctx.fillStyle = '#111';
    ctx.font = '12px Arial';
    values.forEach((value, index) => {
        const barHeight = value * usableHeight / max;
        const x = 45 + index * slot + slot * 0.18;
        const y = height - 35 - barHeight;
        ctx.fillRect(x, y, slot * 0.64, barHeight);
        ctx.fillText(`${value} ${unit}`, x, Math.max(y - 8, 15));
        ctx.fillText(labels[index] || '', x, height - 15);
    });
}

// Cierra el menu movil al seleccionar una opcion.
if (mainNav) {
    mainNav.querySelectorAll('a').forEach((link) => {
        link.addEventListener('click', () => {
            mainNav.classList.remove('open');
            if (menuButton) menuButton.setAttribute('aria-expanded', 'false');
        });
    });
}

// Boton para volver al inicio.
const backToTopButton = document.querySelector('[data-back-to-top]');
if (backToTopButton) {
    window.addEventListener('scroll', () => {
        backToTopButton.classList.toggle('visible', window.scrollY > 500);
    });
    backToTopButton.addEventListener('click', () => window.scrollTo({ top: 0, behavior: 'smooth' }));
}

// Orientacion inicial segun objetivo y disponibilidad.
const goalAdvisorForm = document.getElementById('goalAdvisorForm');
if (goalAdvisorForm) {
    goalAdvisorForm.addEventListener('submit', (event) => {
        event.preventDefault();
        const goal = document.getElementById('trainingGoal').value;
        const days = Number(document.getElementById('availableDays').value);
        const result = document.getElementById('goalAdvisorResult');
        let recommendation = '';

        if (days <= 2) {
            recommendation = 'Te conviene comenzar con sesiones de cuerpo completo y un plan flexible de acceso.';
        } else if (goal === 'fuerza' || goal === 'rendimiento') {
            recommendation = 'Una frecuencia de 4 a 5 días permite distribuir mejor el trabajo y recibir seguimiento más cercano.';
        } else if (goal === 'peso') {
            recommendation = 'Una combinación de fuerza y acondicionamiento 3 a 4 días por semana puede ser un buen punto de partida.';
        } else {
            recommendation = 'Tres sesiones semanales de fuerza y movilidad ofrecen una base equilibrada para mejorar tu salud.';
        }

        result.textContent = `${recommendation} Solicita una clase de prueba para recibir una orientación personalizada.`;
    });
}

// Herramientas de bienestar.
const imcForm = document.getElementById('imcForm');
if (imcForm) {
    imcForm.addEventListener('submit', (event) => {
        event.preventDefault();
        const weight = Number(document.getElementById('weight').value);
        const heightM = Number(document.getElementById('height').value) / 100;
        const bmi = weight / (heightM * heightM);
        let category = 'Obesidad';
        if (bmi < 18.5) category = 'Bajo peso';
        else if (bmi < 25) category = 'Rango saludable';
        else if (bmi < 30) category = 'Sobrepeso';
        document.getElementById('imcResult').textContent = `Tu IMC referencial es ${bmi.toFixed(2)} (${category}).`;
    });
}

const waterForm = document.getElementById('waterForm');
if (waterForm) {
    waterForm.addEventListener('submit', (event) => {
        event.preventDefault();
        const weight = Number(document.getElementById('waterWeight').value);
        const activity = document.getElementById('activityLevel').value;
        const factor = activity === 'high' ? 42 : 35;
        const liters = weight * factor / 1000;
        document.getElementById('waterResult').textContent = `Referencia diaria: aproximadamente ${liters.toFixed(1)} litros de agua.`;
    });
}

const frequencyForm = document.getElementById('frequencyForm');
if (frequencyForm) {
    frequencyForm.addEventListener('submit', (event) => {
        event.preventDefault();
        const experience = document.getElementById('experienceLevel').value;
        const goal = document.getElementById('frequencyGoal').value;
        let days = '3 días por semana';
        let detail = 'con sesiones de cuerpo completo y descanso entre días';

        if (experience === 'intermediate') {
            days = goal === 'strength' ? '4 días por semana' : '3 a 4 días por semana';
            detail = 'alternando tipos de sesión para mantener una recuperación adecuada';
        } else if (experience === 'advanced') {
            days = goal === 'strength' ? '4 a 5 días por semana' : '4 días por semana';
            detail = 'con una planificación individual y control de carga';
        }

        document.getElementById('frequencyResult').textContent = `Sugerencia inicial: ${days}, ${detail}.`;
    });
}

// Busqueda rapida y exportacion de listas para todas las tablas de gestion.
document.querySelectorAll('.data-table').forEach((table, tableIndex) => {
    const wrapper = table.closest('.table-wrap');
    if (!wrapper || wrapper.previousElementSibling?.classList.contains('table-tools')) return;

    const tools = document.createElement('div');
    tools.className = 'table-tools';
    tools.innerHTML = `
        <div class="table-search">
            <label for="tableSearch${tableIndex}">Buscar en la lista</label>
            <input id="tableSearch${tableIndex}" type="search" placeholder="Escribe un nombre, estado o dato" />
        </div>
        <button class="btn btn-secondary" type="button" data-clear-table>Limpiar</button>
        <button class="btn btn-primary" type="button" data-export-table>Exportar lista</button>`;
    wrapper.parentNode.insertBefore(tools, wrapper);

    const searchInput = tools.querySelector('input');
    const rows = Array.from(table.querySelectorAll('tbody tr'));
    const noResults = document.createElement('tr');
    noResults.className = 'table-no-results';
    const columnCount = table.querySelectorAll('thead th').length || 1;
    noResults.innerHTML = `<td colspan="${columnCount}">No se encontraron resultados.</td>`;

    const filterRows = () => {
        const term = searchInput.value.trim().toLocaleLowerCase('es');
        let visible = 0;
        rows.forEach((row) => {
            const match = row.textContent.toLocaleLowerCase('es').includes(term);
            row.hidden = !match;
            if (match) visible += 1;
        });
        noResults.remove();
        if (visible === 0 && table.tBodies[0]) table.tBodies[0].appendChild(noResults);
    };

    searchInput.addEventListener('input', filterRows);
    tools.querySelector('[data-clear-table]').addEventListener('click', () => {
        searchInput.value = '';
        filterRows();
        searchInput.focus();
    });

    tools.querySelector('[data-export-table]').addEventListener('click', () => {
        const visibleRows = Array.from(table.querySelectorAll('tr')).filter((row) => !row.hidden && !row.classList.contains('table-no-results'));
        const csv = visibleRows.map((row) => {
            const cells = Array.from(row.querySelectorAll('th, td'));
            const filteredCells = cells.length > 1 ? cells.slice(0, -1) : cells;
            return filteredCells.map((cell) => `"${cell.textContent.trim().replaceAll('"', '""')}"`).join(',');
        }).join('\n');
        const blob = new Blob(['\ufeff' + csv], { type: 'text/csv;charset=utf-8;' });
        const url = URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = `killing-machine-lista-${new Date().toISOString().slice(0, 10)}.csv`;
        link.click();
        URL.revokeObjectURL(url);
    });
});
