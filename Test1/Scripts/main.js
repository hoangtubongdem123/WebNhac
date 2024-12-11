document.getElementById('openModal').onclick = function () {
    document.getElementById('modal').style.display = 'block';
}

document.querySelectorAll('.openModal').forEach(button => {
    button.onclick = function () {
        const modalId = 'modal-' + this.getAttribute('data-id');
        document.getElementById(modalId).style.display = 'block';
    }
});

document.querySelectorAll('.close').forEach(span => {
    span.onclick = function () {
        document.getElementById('modal').style.display = 'none';
        const modalId = 'modal-' + this.getAttribute('data-id');
        document.getElementById(modalId).style.display = 'none';
    }
});

window.onclick = function (event) {
    if (event.target.classList.contains('modal')) {
        event.target.style.display = 'none';
    }
}