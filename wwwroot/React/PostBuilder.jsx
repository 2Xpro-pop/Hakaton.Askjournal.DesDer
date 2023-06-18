// const { useEffect, useState } = require("react");

// import { useEffect, useState } from "react";
class PostBuilder extends React.Component {
    constructor(props) {
        super(props);
        if (props.fromClient) {
            this.state = { route: props.route };
        }
        else {
            this.state = { parsedData: JSON.parse(props.data) };
        }
    }

    componentDidMount() {
        window.addEventListener("urlChanged", (e) => {
            this.setState({ route: e.detail.route });
        });
    }

    notFound() {
        return {
            blocks: [
                {
                    type: "header",
                    data: {
                        text: "404",
                        level: 1
                    },
                    tunes: {
                        alignment: {
                            alignment: "center"
                        }
                    }
                },
            ]
        }
    }

    render() {
        let data;
        if (this.props.fromClient) {
            if (!window.pages[this.state.route]) {
                data = this.notFound();
            }
            else {
                data = JSON.parse(window.pages[this.state.route].post);
            }
        }
        else {
            data = this.state.parsedData;
        }
        if (!data || data.blocks == undefined || data.blocks == null || data.blocks.length == 0 || data == null) {
            data = this.notFound();
        }
        let tabels = this.props.tabels || tabelsParsed;
        let post = data.blocks.map(block => {
            if (block.type == "paragraph") {
                return <Paragraph data={block.data} tunes={block.tunes} />
            }
            if (block.type == "header") {
                return <Header data={block.data} tunes={block.tunes} />
            }
            if (block.type == "cards") {
                return <Cards data={block.data} tunes={block.tunes} />
            }
            if (block.type == "carousel") {
                return <Carousel data={block.data} tunes={block.tunes} />
            }
            if (block.type == "image") {
                return <Image data={block.data} tunes={block.tunes} />
            }
            if (block.type == "table") {
                console.log("hi!");
                let tb = tabels.find(x => x.id == block.data.id);
                console.log("hi!");
                return (
                    <div className="post-block" >
                        <div className="post-block-content" >
                            <Table headers={tb.headers} data={tb.data} tunes={block.tunes} />
                        </div>
                    </div>

                )
            }
            if (block.type == "quote") {
                return <Quote data={block.data} tunes={block.tunes} />
            }
            if (block.type == "bigCards") {
                return <BigCards data={block.data} tunes={block.tunes} />
            }
            if (block.type == "button") {
                return <Button data={block.data} tunes={block.tunes} />
            }
            if (block.type == "navmenu") {
                return <Navmenu />
            }
            return <div className="post-block">type is {block.type}</div>
        })
        return (
            <div style={{ paddingTop: "100px", paddingBottom: "2rem" }}>
                {post}
            </div>
        )
    }
}
// const PostBuilder = (props) => {
//   const [states, setStates] = useState({
//     route: null,
//     parsedData: null
//   });

//   if (props.fromClient) {
//     setStates({ ...states, route: props.route })
//   } else {
//     setStates({ ...states, parsedData: JSON.parse(props.data) })
//   }

//   useEffect(() => {
//     window.addEventListener("urlChanged", (e) => {
//       setStates({ ...states, route: e.detail.route });
//     });
//   }, [])

//   const notFound =()=> {
//     return {
//       blocks: [
//         {
//           type: "header",
//           data: {
//             text: "404",
//             level: 1
//           },
//           tunes: {
//             alignment: {
//               alignment: "center"
//             }
//           }
//         },
//       ]
//     }
//   }

//   let data;
//   if (props.fromClient) {
//     if (!window.pages[states.route]) {
//       data = notFound();
//     }
//     else {
//       data = JSON.parse(window.pages[states.route].post);
//     }
//   }
//   else {
//     data = states.parsedData;
//   }
//   if (!data || data.blocks == undefined || data.blocks == null || data.blocks.length == 0 || data == null) {
//     data = notFound();
//   }
//   let post = data.blocks.map(block => {
//     const BlockType = capitalizeFirstLetter(block.type);
//     return <BlockType data={block.data} tunes={block.tunes} />
//   })
//   return (
//     <div style={{ paddingTop: "100px", paddingBottom: "2rem" }}>
//       {post}
//     </div>
//   )
// }

function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}