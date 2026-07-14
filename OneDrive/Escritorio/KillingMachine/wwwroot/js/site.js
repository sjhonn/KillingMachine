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
