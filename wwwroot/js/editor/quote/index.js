import { tools } from "./tools.js";

export class QuoteTool {
    cards;
    static get toolbox() {
        return {
            title: "Цитата",
            icon: '<img src="/js/editor/quote/icon.svg" width="17" height="17">',
        };
    }



    constructor({ data, config, api }) {
        this.data = {
            cards: data.cards || [
                { header: "Заголовок", text: "Текст" },
            ],
        };
        this.config = config;
        this.api = api;
        this.cards = document.createElement("div");
        this.cards.className = "row align-items-stretch d-flex justify-content-around mt-2 mb-5 mx-2";
    }

    renderSettings() {
        const wrapper = document.createElement("div");

        tools.forEach((tune) => {
            let button = document.createElement("div");

            button.classList.add("cdx-settings-button");
            button.innerHTML = tune.icon;
            button.onclick = () => tune.action(this);
            wrapper.appendChild(button);
        });

        return wrapper;
    }

    render() {
        for (let i = 0; i < this.data.cards.length; i++) {
            let card = this.data.cards[i];

            this.cards.appendChild(this._createCard(card.header, card.text));
        }

        return this.cards;
    }


    _createCard(header, text) {
        var colDiv = document.createElement("div");
        colDiv.className = "col-sm-12 col-md-4 px-sm-1 px-md-4 mt-3";

        var cardDiv = document.createElement("div");
        cardDiv.className = "p-3 bg-white border rounded";

        var row1Div = document.createElement("div");
        row1Div.className = "row mt-3";

        var openImg = document.createElement("img");
        openImg.src = "/imgs/open.png";
        openImg.className = "me-auto tlp-img";
        openImg.alt = "open";
        openImg.style.width = "50px";
        openImg.style.height = "37px";

        var row2Div = document.createElement("div");
        row2Div.className = "row mt-4";

        var heading4 = document.createElement("h4");
        heading4.setAttribute("contenteditable", "true");
        heading4.textContent = header;

        var row3Div = document.createElement("div");
        row3Div.className = "row mt-2";

        var paragraph = document.createElement("p");
        paragraph.setAttribute("contenteditable", "true");
        paragraph.textContent = text;

        var row4Div = document.createElement("div");
        row4Div.className = "row mt-4";

        var closeImg = document.createElement("img");
        closeImg.src = "/imgs/close.png";
        closeImg.className = "ms-auto tlp-img";
        closeImg.alt = "close";
        closeImg.style.width = "50px";
        closeImg.style.height = "37px";

        // Добавляем элементы в иерархию
        row1Div.appendChild(openImg);

        row2Div.appendChild(heading4);

        row3Div.appendChild(paragraph);

        row4Div.appendChild(closeImg);

        cardDiv.appendChild(row1Div);
        cardDiv.appendChild(row2Div);
        cardDiv.appendChild(row3Div);
        cardDiv.appendChild(row4Div);

        colDiv.appendChild(cardDiv);

        return colDiv;
    }

    save() {
        let data = { cards: [] };
        this.cards.childNodes.forEach((card) => {
            console.log(card.querySelector("h4").innerText);
            data.cards.push(
                {
                    header: card.querySelector("h4").innerText,
                    text: card.querySelector("p").innerText
                }
            )
        });

        return data;
    }
}