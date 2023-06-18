import { CarouselTool } from "./editor/carousel/index.js";
import { CardsTool } from "./editor/cards/index.js";
import { IconsTool } from "./editor/icons/index.js"
import { TableTool } from "./editor/table/index.js"
import { QuoteTool } from "./editor/quote/index.js"
import { BigCardsTool } from "./editor/big-cards/index.js"
import { NavmenuTool } from "./editor/navmenu/index.js"

(function () {
    window.MyEditor = {
        init: function (id) {
            const editor = new EditorJS({
                holder: id,
                tools: {
                    image: {
                        class: window.ImageTool,
                        config: {
                            endpoints: {
                                byFile: "/editor/imageUpload",
                                byUrl: "/editor/fetchUrl",
                            },
                        },
                    },
                    header: {
                        class: Header,
                        tunes: ["alignment"],
                    },
                    embded: {
                        class: Embed,
                        inlineToolbar: true,
                        config: {
                            services: {
                                youtube: true,
                                coub: true,
                                instagram: true,
                                twitter: true,
                                imgur: true,
                                pinterest: true,
                                facebook: true,
                            },
                        },
                    },
                    carousel: {
                        class: CarouselTool,
                        inlineToolbar: true,
                        config: {
                            endpoint: "/editor/imageUpload",
                        },
                    },
                    cards: {
                        class: CardsTool,
                        inlineToolbar: true,
                        tunes: ["alignment"],
                        config: {
                            endpoint: "/editor/imageUpload",
                        },
                    },
                    icons: {
                        class: IconsTool,
                        config: {
                            icons
                        },
                    },
                    paragraph: {
                        class: Paragraph,
                        inlineToolbar: true,
                        tunes: ["alignment"],
                        config: {
                            placeholder: "Введите текст",
                        },
                    },
                    alignment: {
                        class: AlignmentBlockTune,
                        config: {
                            default: "left",
                        },
                    },
                    table: {
                        class: TableTool,
                        config: {
                            tables
                        },
                    },
                    quote: {
                        class: QuoteTool,
                        config: {
                            tables
                        },
                    },
                    bigCards: {
                        class: BigCardsTool,
                    },
                    button: {
                        class: AnyButton,
                        inlineToolbar: false,
                        config: {
                            css: {
                                "btnColor": "btn-primary",
                            }
                        }
                    },
                    navmenu: {
                        class: NavmenuTool,

                    },
                },
                i18n: {
                    messages: {
                        tools: {
                            button: {
                                'Button Text': 'Текс кнопки',
                                'Link Url': 'Ссылка',
                                'Set': "Устоновить",
                                'Default Button': "Беспонтовая кнопка",
                            }
                        }
                    },
                },
                data: postData || undefined,
            });
            console.log("work " + id);
            window.editor = editor;
        },
    };
})();
window.MyEditor.init("editorjs");

document.getElementById("savepost").onclick = function () {
    window.editor.save().then((outputData) => {
        let json = JSON.stringify(outputData);
        let formData = new FormData();
        formData.append("culture", culture);
        formData.append("id", postId);
        formData.append("content", json);
        let request = fetch("/editor/savePost/", {
            method: "POST",
            body: formData,
        });
        request.then(response => {
            console.log(response);
        });
    });
};

document.getElementById("visitpage").onclick = function () {
    window.open(`/${culture}/${postFullPath}`, "_blank").focus();
}