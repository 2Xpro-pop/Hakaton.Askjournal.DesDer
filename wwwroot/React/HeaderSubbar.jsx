class HeaderSubbar extends React.Component {
    constructor(props) {
        super(props);
        this.state = { route: props.route };
    }

    componentDidMount() {
        window.addEventListener("urlChanged", (e) => {
            this.setState({ route: e.detail.route });
        });
    }

    render() {
        let prepath = this.state.route.split("/")[1];
        let post = window.pages[this.state.route];
        let culture = this.props.culture;
        let links = [];

        if (this.state.route == "Main") {
            return (<>
                <h1>{post.header}</h1>
                <p>{post.description}</p>
            </>)
        }
        Object.keys(window.pages).forEach(function (key, index) {
            let pageHeader = window.pages[key].header;
            if(pageHeader == null || pageHeader.trim() === '')
            {
                return;
            }
            if (key.startsWith("Main/" + prepath)) {
                links.push(
                    <a className="btn btn-outline-light text-dark" href={culture + key.substring("Main".length)} onClick={changeUrl}>{pageHeader}</a>
                )
            };
        });
        return (<>
            <h1>{post.header}</h1>
            <p>{post.description}</p>
            {links}
        </>)
    }
}