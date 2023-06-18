window.pages = {};

window.onpopstate = (event) => {
    let customEvent = new CustomEvent('urlChanged', { detail: event.state });
    window.dispatchEvent(customEvent);
}

console.log(document.getElementsByTagName('a'));

let tags = document.getElementsByTagName('a');
for (let i = 0; i < tags.length; i++) {
    tags[i].onclick = changeUrl;
}

let lngTags = document.querySelectorAll('#lng a');
for (let i = 0; i < lngTags.length; i++) {
    lngTags[i].onclick = changeLng;
}

function changeUrl(e) {
    if (e.target.hasAttribute("not-override")) {
        return;
    }
    e.preventDefault();
    console.log(e.target);
    if (e.target.pathname && e.target.pathname.length > 1) {

        window.history.pushState(null, null, e.target.href);

        let culture = window.location.pathname.split('/')[1];
        let path = window.location.pathname.slice(culture.length + 1);
        path = "Main" + (path.length > 1 ? decodeURIComponent(path) : '');

        document.title = window.pages[path].header;

        let customEvent = new CustomEvent('urlChanged', { detail: { route: path } });
        window.dispatchEvent(customEvent);
    }
    else if (e.target.hasAttribute("logo")) {
        window.history.pushState(null, null, e.target.getAttribute("href"));
        path = "Main";
        document.title = window.pages[path].header;

        let customEvent = new CustomEvent('urlChanged', { detail: { route: path } });
        window.dispatchEvent(customEvent);
    }
}

function changeLng(e) {
    e.preventDefault();
    window.location.pathname = e.target.pathname + window.location.pathname.slice(window.location.pathname.indexOf('/', 1));
}

window.addEventListener("urlChanged", (e) => {
    console.log(e.detail);
});

window.onload = async function () {
    let culture = window.location.pathname.split('/')[1];
    let path = window.location.pathname.slice(culture.length + 1);
    window.pages = await (await fetch('post/get-pages/' + culture)).json();
    console.log(pages);
    path = "Main" + (path.length > 1 ? decodeURIComponent(path) : '');
    console.log(path);
    renderPost(path, culture);
}

function renderPost(route, culture) {
    const domContainer = document.querySelector('main');
    const root = ReactDOM.createRoot(domContainer);
    root.render(React.createElement(PostBuilder, { fromClient: true, route }))

    const headerContainer = document.querySelector('div.header-text')
    const header = ReactDOM.createRoot(headerContainer);
    header.render(React.createElement(HeaderSubbar, { fromClient: true, route, culture }))

}