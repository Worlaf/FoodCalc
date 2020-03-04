import Vue from "vue";
import Component from "vue-class-component";

export interface Nutrient {
    name: string;
  }

@Component({
  template: `
      <div>
        <h1>Nutrients</h1>
      </div>
  `
})
export default class NutrientList extends Vue {
  nutrients?: Nutrient[];
}
