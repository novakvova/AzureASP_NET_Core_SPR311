import EnvConfig from "../../../config/env.ts";
import {useEffect, useState} from "react";
import axios from "axios";
import type {ICategoryItem} from "../types.ts";

const CateogriesList : React.FC = () => {

    const urlGet = `${EnvConfig.API_URL}/api/categories/list`;

    const [categories, setCategories] = useState<ICategoryItem[]>([]);

    useEffect(() => {
        getListCategories();
    }, []);

    const getListCategories = async () =>  {
        try
        {
            const result = await axios.get<ICategoryItem[]>(urlGet);
            //console.log("axios result ", result.data);
            setCategories(result.data);
        }
        catch (e)
        {
            console.error("Request data error", e);
        }
    }

    const viewCategories = categories.map((c) => (
        <div key={c.id} className="max-w-sm rounded overflow-hidden shadow-lg">
            <img className="w-full" src={`${EnvConfig.API_URL}${c.imagePath}`}
                 alt="Sunset in the mountains"/>
            <div className="px-6 py-4">
                <div className="font-bold text-xl mb-2">{c.name}</div>
            </div>
            {/*<div className="px-6 pt-4 pb-2">*/}
            {/*    <span*/}
            {/*                className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">#photography</span>*/}
            {/*    <span*/}
            {/*        className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">#travel</span>*/}
            {/*    <span*/}
            {/*        className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">#winter</span>*/}
            {/*</div>*/}
        </div>
    ));

    return (
        <>
            <h1 className={"text-3xl font-bold text-center mb-5 mt-4"}>
                Категорії
            </h1>

            <div>
                {viewCategories}
            </div>
        </>
    )
}

export default CateogriesList;