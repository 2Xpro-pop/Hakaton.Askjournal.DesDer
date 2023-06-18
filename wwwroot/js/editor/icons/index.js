export class IconsTool {
  static get isInline() {
    return true;
  }

  static get sanitize() {
    return {
      i: true,
    };
  }

  constructor({ api, config }) {
    this.api = api;
    this.button = null;
    this._state = false;

    this.tag = "i";
    this.class = "icon";
    this.config = config;
  }

  render() {
    this.button = document.createElement("button");
    this.button.type = "button";
    this.button.innerHTML = '<i class="fa-solid fa-face-grin-wide"></i>';
    this.button.classList.add(this.api.styles.inlineToolButton);

    return this.button;
  }

  surround(range) {
    if (this.state) {
      this.unwrap(range);
      return;
    }

    this.wrap(range);
  }

  wrap(range) {
    const selectedText = range.extractContents();
    const mark = document.createElement("i");

    //mark.appendChild(selectedText);

    mark.classList.add(this.class, "bi");
    mark.setAttribute("contenteditable", "false");
    this.config.icons[0].name.split(" ").forEach((className) => {
      mark.classList.add(className);
    });

    range.insertNode(mark);

    this.api.selection.expandToTag(mark);
  }

  unwrap(range) {
    const mark = this.api.selection.findParentTag("i", "icon");
    const text = range.extractContents();

    mark.remove();

    range.insertNode(text);
  }

  checkState() {
    const mark = this.api.selection.findParentTag(this.tag);

    this.state = !!mark;
  }
}
