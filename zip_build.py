import shutil
import sys

shutil.make_archive(sys.argv[2], "zip", sys.argv[1])