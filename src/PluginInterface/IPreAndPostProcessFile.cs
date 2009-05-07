using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.DataTierGenerator.PluginInterface {
    public interface IPreAndPostProcessFile {

        void PreWriteExisting(String file);
        void PostWriteExisting(String file);
        void PreWriteNew(String file);
        void PostWriteNew(String file);

    }
}
