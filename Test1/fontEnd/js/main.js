const btnPrev = document.querySelector('.btnPrev');
const btnNext = document.querySelector('.btnNext');
const gallery = document.querySelector('.gallery');
const mainAppBody = document.getElementById('mainAppBody');




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

const listsong = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(ViewBag.ListSongs));



const audio = getElementById('audio');
const pause = getElementById('pause');
const play = getElementById('play');






