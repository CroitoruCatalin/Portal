
import './styles/quasar.scss'
//import '@quasar/extras/roboto-font/roboto-font.css'
//import '@quasar/dist/types/extras/material-icons/material-icons.css'
//import '@quasar/extras/fontawesome-v5/fontawesome-v5.css'
import { type QuasarPluginOptions } from 'quasar';
import * as Quasar from 'quasar';
import 'quasar/dist/quasar.css';

const options: QuasarPluginOptions = {
  config: {
  },
  plugins: {
    Dialog: Quasar.Dialog,
    Notify: Quasar.Notify,
  },
  components: {
    QBtn: Quasar.QBtn,
    QToolbar: Quasar.QToolbar,
    QIcon: Quasar.QIcon,
    QDrawer: Quasar.QDrawer,
    QLayout: Quasar.QLayout,
    QHeader: Quasar.QHeader,
    QPage: Quasar.QPage,
    QPageContainer: Quasar.QPageContainer,
    QList: Quasar.QList,
    QItem: Quasar.QItem,
    QItemSection: Quasar.QItemSection,
    QItemLabel: Quasar.QItemLabel,
    QToolbarTitle: Quasar.QToolbarTitle,
    QCardSection: Quasar.QCardSection,
    QInput: Quasar.QInput,
    QCheckbox: Quasar.QCheckbox,
    QForm: Quasar.QForm,
    QCardActions: Quasar.QCardActions,
    QCard: Quasar.QCard,
  }
};
export default options;
