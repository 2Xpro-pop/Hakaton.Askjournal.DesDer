export class TableTool {
    select;
    static get toolbox() {
        return {
            title: "Таблицы",
            icon: '<img src="/js/editor/table/icon.svg" width="17" height="17">',
        };
    }

    constructor({ data, config, api }) {
        this.data = data;
        this.config = config;
        this.api = api;
        this.select = document.createElement("select");;
        this.select.className = "tlp-select-box";
    }


    render() {
        var containerDiv = document.createElement("div");
        containerDiv.className = "tlp-container";

        var selectContainerDiv = document.createElement("div");
        selectContainerDiv.className = "tlp-select-container";

        var select = this.select;
        var names = this.config.tables;
        console.log(this.data);
        if (!this.data.id) {
            var defaultOption = document.createElement("option");
            defaultOption.value = "";
            defaultOption.textContent = "Таблицы";
            defaultOption.selected = true;
            defaultOption.disabled = true;
            defaultOption.hidden = true;

            select.appendChild(defaultOption);
        }
        else {
            
        }
        

        for(var i = 0; i < names.length; i++) {
            var option = document.createElement("option");
            option.value = names[i].Id;
            option.textContent = names[i].Name;
            if (names[i].Id == this.data.Id) option.selected = true;
            select.appendChild(option);
        }

        var iconContainerDiv = document.createElement("div");
        iconContainerDiv.className = "tlp-icon-container";

        var tableIcon = document.createElement("img");
        tableIcon.src = "/imgs/cells.png";
        tableIcon.alt = "table icon";

        iconContainerDiv.appendChild(tableIcon);

        selectContainerDiv.appendChild(select);
        selectContainerDiv.appendChild(iconContainerDiv);

        containerDiv.appendChild(selectContainerDiv);


        return containerDiv;
    }

    save() {
        if (this.select.value.trim() === "") {
            return null;
        }
        return { id: this.select.value };
    }
}