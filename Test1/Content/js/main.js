const btnPrev = document.querySelector('.btnPrev');
const btnNext = document.querySelector('.btnNext');
const gallery = document.querySelector('.gallery');
const mainAppBody = document.getElementById('mainAppBody');

console.log(mainAppBody);
console.log(2);


const galleryWidth = gallery.offsetWidth;


gallery.addEventListener('wheel', (e) => {

    e.preventDefault();
    gallery.scrollLeft += e.deltaY;
})

btnNext.addEventListener("click", () => {
    console.log(galleryWidth);
    gallery.scrollLeft += galleryWidth;
})
btnPrev.addEventListener('click', () => {
    gallery.scrollLeft -= galleryWidth;
})

// sự kiện scroll màn hình

mainAppBody.addEventListener('scroll', () => {
    const scrollPx = mainAppBody.scrollTop;
    console.log(scrollPx);
    if (scrollPx === 0)
        appHeader.classList.remove("onscroll");
    else {
        appHeader.classList.add("onscroll");
    }
})