class Quote extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="post-block">
                <div className="post-block-content" >
                    <div
                        className="row align-items-stretch d-flex justify-content-around mt-2 mb-5 mx-2"
                        style={{ paddingTop: '15px' }} >
                        {this.props.data.cards.map((card) => {
                            return (
                                <div className="col-sm-12 col-md-4 px-sm-1 px-md-4 mt-3">
                                    <div className="p-3 bg-white border rounded">
                                        <div className="row mt-3">
                                            <img
                                                src="/imgs/open.png"
                                                class="me-auto tlp-img"
                                                alt="open"
                                                style={{ width: '50px', height: '37px' }}
                                            />
                                        </div>
                                        <div className="row mt-4">
                                            <h4>{card.header}</h4>
                                        </div>
                                        <div className="row mt-2">
                                            <p>{card.text}</p>
                                        </div>
                                        <div className="row mt-4">
                                            <img
                                                src="/imgs/close.png"
                                                className="ms-auto tlp-img"
                                                alt="close"
                                                style={{ width: '50px', height: '37px' }}
                                            />
                                        </div>
                                    </div>
                                </div>)
                        })}
                    </div>
                </div>
            </div>);
    }
}